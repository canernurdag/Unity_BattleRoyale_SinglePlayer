using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Zenject.Tests
{
    public class TestSceneContextEvents : SceneTestFixture
    {
        [UnityTest]
        public IEnumerator TestScene()
        {
            yield return LoadScene("TestSceneContextEvents");
            yield return new WaitForSeconds(2.0f);
        }
    }
}
