using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public GameObject FoodPrefab;
    public Transform FoodRoot;
    public float PlayerSpeed = 4.0f;
    public float AddMass = 4.0f;
    

    private List<GameObject> _objectsMotion = new List<GameObject>();

    private void Start()
    {
        for (var i = 0; i < 100; i++) 
        {
            var vect = new Vector3(Random.Range(-25.0f, 25.0f), Random.Range(-25.0f, 25.0f), Random.Range(-25.0f, 25.0f));
            var food = Instantiate(FoodPrefab, vect, Quaternion.identity);
            food.transform.SetParent(FoodRoot);
            _objectsMotion.Add(food);
        }
    }

    private void Update()
    {
        var movementDirection = Vector3.ClampMagnitude(
            new Vector3(
                Input.GetAxis("Horizontal"),
                (Input.GetKey(KeyCode.Space) ? 1.0f : 0.0f) + (Input.GetKey(KeyCode.LeftShift) ? -1.0f : 0.0f),
                Input.GetAxis("Vertical")),
            1.0f);
        
        for (var i = 0; i < _objectsMotion.Count; i++) 
        {
            var worldObject = _objectsMotion[i];
            worldObject.transform.Translate(movementDirection * -PlayerSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        transform.localScale *= AddMass;
        _objectsMotion.Remove(collider.gameObject);
        Destroy(collider.gameObject);
    }
}
