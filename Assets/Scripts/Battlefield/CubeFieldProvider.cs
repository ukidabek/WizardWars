using UnityEngine;
namespace Battlefield
{
    public class CubeFieldProvider : FieldPrefabProvider
    {
        public override GameObject Field => Create();

        private GameObject Create()
        {
            var gameObject = new GameObject("Field", typeof(Field));
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(gameObject.transform);
            cube.transform.localPosition = Vector3.one * .5f;
            Destroy(cube.GetComponent<Collider>());
            var box = gameObject.AddComponent<BoxCollider>();
            box.isTrigger = true;
            box.ClosestPoint(Vector3.one * .5f);
            return gameObject;
        }
    }
}
