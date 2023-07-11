using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class MotiveToUnityMapper : MonoBehaviour
{
    // the scene model to be moved according to motion capture data
    public GameObject destGameObject;
    public TextAsset motiveSkeletonXML;
    
    // Lookup map for Motive skeleton bone IDs (keys) and their corresponding GameObjects in Unity (values).
    private Dictionary<string, GameObject> _boneMap = new();
    private GameObject _rootObj;
    private string _skeletonName;
    
    public GameObject leftUpperArm;
    public GameObject leftLowerArm;
    public GameObject leftHand;
    
    /**
    * Parse the input moCapSkeletonXML file and creates a Unity skeleton accordingly.
     * It parses the skeleton name, stored bone IDs, their hierarchy and offsets. The skeleton uses the public
     * _skeletonRootObj as its root object. All bones are also stored in the _boneObjectMap with their ID and GameObject
     * for later lookup.
    */
    private void Start()
    {
        var moCapSkeleton = XDocument.Parse(motiveSkeletonXML.text);

        // Parse skeleton name from XML
        var nameQuery = from c in moCapSkeleton.Root.Descendants("property")
            where c.Element("name").Value == "NodeName"
            select c.Element("value").Value;
        var skeletonName = nameQuery.First();

        // Parse all bones with parents and offsets
        var bonesQuery = from c in moCapSkeleton.Root.Descendants("bone")
            select (c.Attribute("id").Value,
                c.Element("offset").Value.Split(","),
                c.Element("parent_id").Value);

        // create a new object if it doesn't exist yet 
        if (!_rootObj)
        {
            _rootObj = new GameObject("Generated_" + skeletonName);
            // attach it to the dest game object
            var parentTransform = destGameObject.transform;
            _rootObj.transform.SetPositionAndRotation(
                parentTransform.position,
                parentTransform.rotation
            );
            _rootObj.transform.parent = parentTransform;
        }

        // recreate Motive skeleton structure in Unity
        foreach (var bone in bonesQuery)
        {
            // transform the XML ID to a Mechanim ID for the lookup map
            var mKey = XmlIDtoMecanimID(bone.Item1);

            // create a new object if it doesn't exist yet (might already exist in case we re-parse the XML)
            if (!_boneMap.ContainsKey(mKey))
                _boneMap[mKey] = new GameObject(skeletonName + "_" + mKey);
            //the bone with parent 0 is the root object
            _boneMap[mKey].transform.parent = bone.Item3 == "0"
                ? _rootObj.transform
                : _boneMap[XmlIDtoMecanimID(bone.Item3)].transform;
            // apply the parsed offsets
            _boneMap[mKey].transform.localPosition = new Vector3(
                float.Parse(bone.Item2[0]),
                float.Parse(bone.Item2[1]),
                float.Parse(bone.Item2[2]));
        }
    }
    
    
    private void LateUpdate()
    {
        var uat = _boneMap["LeftUpperArm"].transform;
        var lat = _boneMap["LeftLowerArm"].transform;
        var ht = _boneMap["LeftHand"].transform;

        // leftUpperArm.transform.SetPositionAndRotation(uat.position, uat.rotation * Quaternion.Euler(0, 0, 90));
        // leftLowerArm.transform.SetPositionAndRotation(lat.position, lat.rotation * Quaternion.Euler(0, 0, 90));
        // leftHand.transform.SetPositionAndRotation(ht.position, ht.rotation * Quaternion.Euler(0, 0, 90));

        leftUpperArm.transform.rotation = (uat.rotation * Quaternion.Euler(0, 0, 90));
        leftLowerArm.transform.rotation = (lat.rotation * Quaternion.Euler(0, 0, 90));
        leftHand.transform.rotation = (ht.rotation * Quaternion.Euler(0, 0, 90));
    }
    
    /**
     * IDs in the XML of Motive are distinct from the XML or Mechanim IDs.
     * Use this to map from CSV ID to Unity Mechanim ID.
     */
    public static string XmlIDtoMecanimID(string xmlId)
    {
        var dict = new Dictionary<string, string>()
        {
            { "1", "Hips" },
            { "2", "Spine" },
            { "3", "Chest" },
            { "4", "Neck" },
            { "5", "Head" },

            { "6", "LeftShoulder" },
            { "7", "LeftUpperArm" },
            { "8", "LeftLowerArm" },
            { "9", "LeftHand" },

            { "10", "RightShoulder" },
            { "11", "RightUpperArm" },
            { "12", "RightLowerArm" },
            { "13", "RightHand" },

            { "14", "LeftUpperLeg" },
            { "15", "LeftLowerLeg" },
            { "16", "LeftFoot" },
            { "17", "LeftToes" },

            { "18", "RightUpperLeg" },
            { "19", "RightLowerLeg" },
            { "20", "RightFoot" },
            { "21", "RightToes" }
        };
        return dict[xmlId];
    }

    public Dictionary<string, GameObject> GetBoneMap()
    {
        return _boneMap;
    }
}