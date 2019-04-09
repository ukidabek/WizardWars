using UnityEngine;
namespace Battlefield
{
    public class CubeFieldProvider : FieldPrefabProvider
    {
        public override GameObject Prefab => Create();

        private GameObject Create()
        {
            var gameObject = new GameObject("Field");
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(gameObject.transform);
            cube.transform.localPosition = Vector3.one * .5f;
            return gameObject;
        }
    }
}
