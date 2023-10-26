using System;
using UnityEngine;
using SharpNeat.Phenomes;
using UnityEngine.Serialization;


public class ProjectileScript : MonoBehaviour
{
    [HideInInspector] public float maxSpeed;
    
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    public Transform parentTransform;
    
    
    [SerializeField] [Range(10f, 100f)] float maxDistance = 10;
    public float distance;
    private float _birthTime;
    public float lifespan;

    public float sign;
    public IBlackBox Box;
    private ISignalArray _inputArr;
    private ISignalArray _outputArr;

    public Vector2 relativePos;
    
    private GameObject _emptyGameObject;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _emptyGameObject = new GameObject();
        parentTransform = _emptyGameObject.transform;
        maxDistance = 15;
    }

    private void Start()
    {
        _inputArr = Box.InputSignalArray;
        _outputArr = Box.OutputSignalArray;
        lifespan = 10f;
        _birthTime = Time.time;
        
    }


    void DestroyYourself()
    {
        
        if (Time.time - _birthTime > lifespan)
        {
            Destroy(_emptyGameObject);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Box.ResetState();
        
        relativePos = parentTransform.InverseTransformPoint(transform.position);
        distance = Mathf.Sqrt(Vector2.SqrMagnitude(relativePos));

        _inputArr[0] = Mathf.Lerp(-1f, 1f,Math.Abs(relativePos.x) / maxDistance);
        
        if (relativePos.y > 0)
            _inputArr[1] = Mathf.Lerp(0f, 1f,relativePos.y / maxDistance);
        else
            _inputArr[1] = Mathf.Lerp(-1f, 0f,Math.Abs(relativePos.y) / maxDistance);
        
        
        _inputArr[2] = Mathf.Lerp(-1f, 1f,distance / maxDistance);
        
        
        Box.Activate();

        float x = ((float)_outputArr[0] * 2 - 1) * sign;
        float y = ((float)_outputArr[1] * 2 - 1);

        Vector2 vel = x * parentTransform.right + y * parentTransform.up;

        float R, G, B;
        R = (float)_outputArr[2] * 255; 
        G = (float)_outputArr[3] * 255; 
        B = (float)_outputArr[4] * 255;
        
        _spriteRenderer.color = new Color(R, G, B);
        
        _rigidbody.velocity += vel;

        float speed = _rigidbody.velocity.magnitude;
        if (speed > maxSpeed)
            _rigidbody.velocity = (_rigidbody.velocity / speed) * maxSpeed;
        
        
        transform.up = Vector3.Lerp(transform.up, _rigidbody.velocity, speed / maxSpeed);
        
        
        DestroyYourself();

    }
}
