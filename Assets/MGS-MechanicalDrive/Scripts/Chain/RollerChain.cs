/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: RollerChain.cs
 *  Author: Mogoson   Version: 1.0   Date: 6/21/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.         RollerChain              Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     6/21/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.MechanicalDrive
{
    using UnityEngine;

    [AddComponentMenu("Developer/MechanicalDrive/RollerChain")]
    public class RollerChain : Chain
    {
        #region Property and Field
        /// <summary>
        /// Roller prefab of chain.
        /// </summary>
        public GameObject rollerPrefab;
        #endregion

        #region Public Method
        /// <summary>
        /// Create chain nodes.
        /// </summary>
        public override void CreateNodes()
        {
            nodes = new Node[count];
            bool replace = false;
            for (int i = 0; i < count; i++)
            {
                //Alternate prefab.
                var prefab = nodePrefab;
                if (replace)
                    prefab = rollerPrefab;

                //Create node.
                var nodeClone = (GameObject)Instantiate(prefab, nodeRoot);
                TowNodeBaseOnCurve(nodeClone.transform, i * space);

                //Set node ID.
                nodes[i] = nodeClone.GetComponent<Node>();
                nodes[i].ID = i;

                //Alternate replace.
                replace = !replace;
            }
        }
        #endregion
    }//class_end
}//namespace_end