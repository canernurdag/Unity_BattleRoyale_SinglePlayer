using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Network
{
    public class ManagerNetwork : MonoBehaviour
    {

        //REPLICATE THE NEEDED NETWORK BEHAVIOURS
        // TO BE CHANGED WHEN THE MULTIPLAYER SOLUTION IS IMPLEMENTED

        public List<int> ActiveUserUniqueIds = new();

        private void Awake()
        {
            //Init current player
            ActiveUserUniqueIds.Add(1);
        }

        public int GetNewUniqueUserId()
        {
            int lastUserId = ActiveUserUniqueIds[^1];
            int newUserId = lastUserId + 1;

            ActiveUserUniqueIds.Add(newUserId);
            return newUserId;
        }

        public void RemoveActiveUser(int userId)
        {
            bool isActiveUserExist = ActiveUserUniqueIds.Any(x => x == userId);
            if (isActiveUserExist)
            {
                ActiveUserUniqueIds.Remove(userId);
            }
            else if (!isActiveUserExist)
            {
                Debug.Log("Logic Error");
            }

        }
    }
}