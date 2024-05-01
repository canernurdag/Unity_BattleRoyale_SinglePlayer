#define AUTO_SWITCH_SHADERS_TO_CURRENT_PIPELINE
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CW.Common
{
	/// <summary>This asset stores multiple variants of a shader for different rendering pipelines, and allows you to switch between them in the editor.</summary>
	//[CreateAssetMenu(fileName = "NewShaderBundle", menuName = "MakeShaderBundle", order = 1)]
	public class CwShaderBundle : ScriptableObject
	{
		public enum Pipeline
		{
			Invalid  = -1,
			Standard,
			URP2019,
			URP2020,
			URP2021,
			HDRP2019,
			HDRP2020,
			HDRP2021,
			COUNT
		}

		/// <summary>This stores the information unique to each shader variant.</summary>
		[System.Serializable]
		public class ShaderVariant
		{
			public Pipeline Pipe;
			public string   Code;
			public int      Hash;
			public bool     Dirty;

			public string HashString
			{
				get
				{
					return "//<HASH>" + Hash + "</HASH>";
				}
			}
		}

		/// <summary>The title of the generated shader.</summary>
		public string Title { set  { title = value; } get { return title; } } [SerializeField] private string title;

		/// <summary>The shader that will be modified to work with the selected rendering pipeline.</summary>
		public Shader Target { set  { target = value; } get { return target; } } [SerializeField] private Shader target;

		/// <summary>The hash code of the currently loaded bundle.</summary>
		public int VariantHash { set  { variantHash = value; } get { return variantHash; } } [SerializeField] private int variantHash;

		/// <summary>The hash code of the current device to make sure the shaders have been loaded properly.</summary>
		public int ProjectHash { set  { projectHash = value; } get { return projectHash; } } [SerializeField] private int projectHash;

		/// <summary>This stores all shader variants for this shader.</summary>
		public List<ShaderVariant> Variants { get { if (variants == null) variants = new List<ShaderVariant>(); return variants; } } [SerializeField] private List<ShaderVariant> variants;

		public bool Dirty
		{
			get
			{
				if (variants != null)
				{
					foreach (var variant in variants)
					{
						if (variant.Dirty == true)
						{
							return true;
						}
					}
				}

				return false;
			}
		}

#if UNITY_EDITOR && AUTO_SWITCH_SHADERS_TO_CURRENT_PIPELINE
		private static Pipeline lastAutoSetPipe = Pipeline.Invalid;
#endif
		public static int GetProjectHash()
		{
			return SystemInfo.deviceUniqueIdentifier.GetHashCode() ^ Application.dataPath.GetHashCode();
		}

		/// <summary>This tells you which rendering pipeline the project is currently using.</summary>
		public static Pipeline DetectProjectPipeline()
		{
			var crp = UnityEngine.Rendering.GraphicsSettings.currentRenderPipeline;

			if (crp != null)
			{
				var title = crp.GetType().ToString();

				if (title.Contains("HighDefinition") == true)
				{
#if UNITY_2021_2_OR_NEWER
					return Pipeline.HDRP2021;
#elif UNITY_2020_2_OR_NEWER
					return Pipeline.HDRP2020;
#else
					return Pipeline.HDRP2019;
#endif
				}
				else// if (title.Contains("Universal") == true)
				{
#if UNITY_2021_2_OR_NEWER
					return Pipeline.URP2021;
#elif UNITY_2020_2_OR_NEWER
					return Pipeline.URP2020;
#else
					return Pipeline.URP2019;
#endif
				}
			}
			else
			{
				return Pipeline.Standard;
			}

			//return Pipeline.Invalid;
		}

		public static bool IsStandard(Pipeline pipe)
		{
			return pipe == Pipeline.Standard;
		}

		public static bool IsScriptable(Pipeline pipe)
		{
			return IsURP(pipe) || IsHDRP(pipe);
		}

		public static bool IsURP(Pipeline pipe)
		{
			return pipe == Pipeline.URP2019 || pipe == Pipeline.URP2020 || pipe == Pipeline.URP2021;
		}

		public static bool IsHDRP(Pipeline pipe)
		{
			return pipe == Pipeline.HDRP2019 || pipe == Pipeline.HDRP2020 || pipe == Pipeline.HDRP2021;
		}

#if UNITY_EDITOR
		/// <summary>This will automatically update all shaders when assemblies reload.</summary>
		[InitializeOnLoadMethod]
		private static void RegisterAutoSwitch()
		{
			UnityEditor.EditorApplication.delayCall -= AutoSwitch;
			UnityEditor.EditorApplication.delayCall += AutoSwitch;

			AssetDatabase.importPackageCompleted -= ForceSwitch;
			AssetDatabase.importPackageCompleted += ForceSwitch;
		}

		private static void ForceSwitch(string name)
		{
			lastAutoSetPipe = Pipeline.Invalid;

			AutoSwitch();
		}

		private static void AutoSwitch()
		{
	#if AUTO_SWITCH_SHADERS_TO_CURRENT_PIPELINE
			var pipe = DetectProjectPipeline();
			var hash = GetProjectHash();

			if (lastAutoSetPipe != pipe)
			{
				lastAutoSetPipe = pipe;

				var modified = false;
				var guids    = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(CwShaderBundle).Name);

				foreach (var guid in guids)
				{
					var path   = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
					var bundle = UnityEditor.AssetDatabase.LoadAssetAtPath<CwShaderBundle>(path);

					if (bundle != null && bundle.variantHash != 0)
					{
						if (bundle.projectHash != hash)
						{
							if (bundle.TrySwitchTo(pipe) == true)
							{
								modified = true;

								continue;
							}
						}
						else
						{
							foreach (var variant in bundle.Variants)
							{
								// Correct pipe, but out of sync shader
								if (variant.Pipe == pipe && variant.Hash != bundle.variantHash)
								{
									if (bundle.TrySwitchTo(pipe) == true)
									{
										modified = true;

										continue;
									}
								}
								// Wrong pipe
								else if (variant.Pipe != pipe && variant.Hash == bundle.variantHash)
								{
									if (bundle.TrySwitchTo(pipe) == true)
									{
										modified = true;

										continue;
									}
								}
							}
						}
					}
				}

				if (modified == true)
				{
					Debug.Log(typeof(CwShaderBundle).Name + " changed shaders to pipeline: " + pipe);
				}
			}

			UnityEditor.EditorApplication.delayCall -= AutoSwitch;
			UnityEditor.EditorApplication.delayCall += AutoSwitch;
	#endif
		}

	#if __BETTERSHADERS__
		private string GetPath()
		{
			var raw  = UnityEditor.AssetDatabase.GetAssetPath(this);
			var path = System.IO.Path.ChangeExtension(raw, "surfshader");

			if (System.IO.File.Exists(path) == true)
			{
				return path;
			}

			return System.IO.Path.ChangeExtension(raw, "stackedshader");
		}

		public void Compile()
		{
			if (target != null)
			{
				UnityEditor.Undo.RecordObject(this, "Compiling Shaders");

				if (variants == null)
				{
					variants = new List<ShaderVariant>();
				}
				else
				{
					variants.Clear();
				}

				var overrides = new JBooth.BetterShaders.OptionOverrides();
				var path      = GetPath();

				overrides.shaderName = title;

				if (System.IO.File.Exists(path) == true)
				{
					for (var i = 0; i < (int)Pipeline.COUNT; i++)
					{
						// Skip non-implemented pipelines
						//if (i == (int)Pipeline.URP2021)
						//{
						//	continue;
						//}

						CompileVariant((Pipeline)i, overrides, path);
					}
				}
				else
				{
					Debug.LogError("Failed to compile shader bundle because of missing file: " + path);
				}

				UnityEditor.EditorUtility.SetDirty(this);
			}
		}

		public void TryCompileFast()
		{
			var pipe = DetectProjectPipeline();

			foreach (var variant in variants)
			{
				if (variant.Pipe == pipe)
				{
					var overrides = new JBooth.BetterShaders.OptionOverrides();
					var path      = GetPath();

					overrides.shaderName = title;

					// Reset all hashes to mark as dirty
					foreach (var v in variants)
					{
						v.Dirty = true;
					}

					if (System.IO.File.Exists(path) == true)
					{
						CompileVariant(overrides, path, variant);
					}
					else
					{
						Debug.LogError("Failed to compile shader bundle because of missing file: " + path);
					}

					return;
				}
			}
		}

		private void CompileVariant(Pipeline pipe, JBooth.BetterShaders.OptionOverrides overrides, string path)
		{
			var variant = new ShaderVariant();

			variant.Pipe = pipe;

			CompileVariant(overrides, path, variant);

			variants.Add(variant);
		}

		private void CompileVariant(JBooth.BetterShaders.OptionOverrides overrides, string path, ShaderVariant variant)
		{
			var pipeline = (JBooth.BetterShaders.ShaderBuilder.RenderPipeline)variant.Pipe;

			if (path.EndsWith("surfshader"))
			{
				variant.Code = JBooth.BetterShaders.BetterShaderImporterEditor.BuildExportShader(pipeline, overrides, path);
			}
			else if (path.EndsWith("stackedshader"))
			{
				variant.Code = JBooth.BetterShaders.StackedShaderImporterEditor.BuildExportShader(pipeline, overrides, path);
			}

			variant.Hash  = Random.value.GetHashCode() ^ variant.Code.GetHashCode();
			variant.Code  = variant.Code.Replace("\n\r", "\n");
			variant.Code  = variant.Code.Replace("\r\n", "\n");
			variant.Code  = variant.HashString + "\n" + variant.Code;
			variant.Dirty = false;
		}
	#endif

		public bool TrySwitchTo(Pipeline pipe)
		{
			if (variants != null && target != null)
			{
				foreach (var variant in variants)
				{
					if (variant.Pipe == pipe)
					{
						var path = UnityEditor.AssetDatabase.GetAssetPath(target);

						// Already up to date?
						if (System.IO.File.ReadAllText(path) != variant.Code)
						{
							System.IO.File.WriteAllText(path, variant.Code);

							UnityEditor.AssetDatabase.ImportAsset(path);

							UnityEditor.Undo.RecordObject(this, "Switching Pipeline");

							variantHash = variant.Hash;
							projectHash = GetProjectHash();

							UnityEditor.EditorUtility.SetDirty(this);

							return true;
						}

						break;
					}
				}
			}

			return false;
		}

		public static void TrySwitchAllTo(Pipeline pipe)
		{
			var guids = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(CwShaderBundle).Name);

			foreach (var guid in guids)
			{
				var path   = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
				var bundle = UnityEditor.AssetDatabase.LoadAssetAtPath<CwShaderBundle>(path);

				if (bundle != null)
				{
					bundle.TrySwitchTo(pipe);
				}
			}
		}
#endif
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(CwShaderBundle))]
	public class CwShaderBundle_Editor : Editor
	{
		private CwShaderBundle tgt;

		public override void OnInspectorGUI()
		{
			tgt = (CwShaderBundle)target;

			DrawShader();

	#if __BETTERSHADERS__
			EditorGUILayout.Separator();

			DrawCompile();
	#endif

			EditorGUILayout.Separator();

			DrawShaderBundles();

			serializedObject.ApplyModifiedProperties();
		}

		private void DrawShader()
		{
			var pipe = CwShaderBundle.DetectProjectPipeline();

			EditorGUILayout.PropertyField(serializedObject.FindProperty("target"));

			EditorGUI.BeginDisabledGroup(true);
				EditorGUILayout.TextField("Project Pipeline", pipe.ToString());

				foreach (var variant in tgt.Variants)
				{
					var active = variant.Hash == tgt.VariantHash;

					EditorGUILayout.Toggle(variant.Pipe.ToString(), active);
				}
			EditorGUI.EndDisabledGroup();

			EditorGUILayout.Separator();

			if (GUILayout.Button("Switch Shader To " + pipe.ToString()) == true)
			{
				tgt.TrySwitchTo(pipe);

				serializedObject.Update();
			}

			if (GUILayout.Button("Switch All Shaders To " + pipe.ToString()) == true)
			{
				CwShaderBundle.TrySwitchAllTo(pipe);

				serializedObject.Update();
			}
		}

	#if __BETTERSHADERS__
		private void DrawCompile()
		{
			EditorGUILayout.LabelField("Better Shaders", EditorStyles.boldLabel);

			EditorGUILayout.PropertyField(serializedObject.FindProperty("title"));

			EditorGUILayout.Separator();

			if (GUILayout.Button("Fast Compile") == true)
			{
				tgt.TryCompileFast();
				tgt.TrySwitchTo(CwShaderBundle.DetectProjectPipeline());

				serializedObject.Update();
			}

			if (GUILayout.Button("Compile") == true)
			{
				tgt.Compile();
				tgt.TrySwitchTo(CwShaderBundle.DetectProjectPipeline());

				serializedObject.Update();
			}

			if (GUILayout.Button("Compile All") == true)
			{
				var guids = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(CwShaderBundle).Name);

				foreach (var guid in guids)
				{
					var path   = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
					var bundle = UnityEditor.AssetDatabase.LoadAssetAtPath<CwShaderBundle>(path);

					if (bundle != null)
					{
						bundle.Compile();
					}
				}

				serializedObject.Update();
			}
		}
	#endif

		private void DrawShaderBundles()
		{
			EditorGUILayout.LabelField("All Shaders In Project", EditorStyles.boldLabel);

			var guids = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(CwShaderBundle).Name);
			var color = GUI.color;

			EditorGUI.BeginDisabledGroup(true);
				foreach (var guid in guids)
				{
					var path   = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
					var bundle = UnityEditor.AssetDatabase.LoadAssetAtPath<CwShaderBundle>(path);

					if (bundle != null)
					{
						var title  = bundle.Target != null ? bundle.Target.name : "<MISSING>";

						GUI.color = bundle.Dirty == true ? Color.red : color;
					
						EditorGUILayout.ObjectField(title, bundle, typeof(CwShaderBundle), false);

						GUI.color = color;

						EditorGUILayout.Separator();
					}
				}
			EditorGUI.EndDisabledGroup();

			GUI.color = color;
		}
	}
#endif

#if UNITY_EDITOR
	#if __BETTERSHADERS__
	/// <summary>This will automatically rebuild a shader bundle when its source better shader is modified.</summary>
	public class CwShaderBundleDetector : AssetPostprocessor
	{
		void OnPreprocessAsset()
		{
			TryCompileFast(".surfshader");
			TryCompileFast(".stackedshader");
		}

		private void TryCompileFast(string end)
		{
			var shaderPath = assetImporter.assetPath;

			if (shaderPath.EndsWith(end) == true)
			{
				var bundlePath = System.IO.Path.ChangeExtension(shaderPath, "asset");
				var bundle     = AssetDatabase.LoadAssetAtPath<CwShaderBundle>(bundlePath);

				if (bundle != null)
				{
					var pipe = CwShaderBundle.DetectProjectPipeline();

					foreach (var variant in bundle.Variants)
					{
						if (variant.Pipe == pipe)
						{
							if (System.IO.File.ReadAllText(shaderPath).Contains(variant.HashString) == true)
							{
								return;
							}
						}
					}

					bundle.TryCompileFast();
					bundle.TrySwitchTo(pipe);
				}
			}
		}
	}
	#endif
#endif
}