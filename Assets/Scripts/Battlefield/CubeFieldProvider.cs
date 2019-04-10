using UnityEngine;
namespace Battlefield
{
    public class CubeFieldProvider : FieldPrefabProvider
    {
        public override GameObject Field => Create();

        private GameObject Create()
        {
            // Parent object.
            var gameObject = new GameObject("Field", typeof(Field));

            // child
            var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            quad.transform.localRotation = Quaternion.Euler(90, 0, 0);
            quad.transform.SetParent(gameObject.transform);
            quad.transform.localPosition = new Vector3(.5f, .01f, .5f);
            Destroy(quad.GetComponent<Collider>());

            gameObject.GetComponent<Field>().FieldTransfomr = quad.transform;

            //  Collider
            var boxCollider = gameObject.AddComponent<BoxCollider>();
            boxCollider.isTrigger = true;
            boxCollider.center = Vector3.one * .5f;
            return gameObject;
        }
    }
}
