using System.Collections;
using SharpNeat.Decoders.Neat;
using SharpNeat.Genomes.Neat;
using SharpNeat.Network;
using SharpNeat.Phenomes;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] [Range(0, 10)] private float speed;

    public GameObject projectilePrefab;
    
    public NeatGenome ProjectileGenome;
    public uint id;
    public uint birthGeneration;
    public double complexity;
    public bool isEvaluated;
    public INodeList NodeList;
    public int nodes;
    
    public Transform parentTransform;
    public NeatGenomeDecoder Decoder;
    
    private IBlackBox _box;
    private ISignalArray _inputArr;
    private ISignalArray _outputArr;
    
    private void Awake()
    {
        speed = 5;
    }
    
    private void OnValidate()
    {
        if(!Application.isPlaying) return;
        if (isEvaluated)
            ProjectileGenome.EvaluationInfo.SetFitness(10);
    }
    
    IEnumerator FireProjectile()
    {
        while (true)
        {
            _box = Decoder.Decode(ProjectileGenome);
             for (int i = 0; i < 2; i++)
             {
                float sign = (i % 2 == 0) ? -1 : 1;
                GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity, parentTransform);
                ProjectileScript newProjectileScript = newProjectile.GetComponent<ProjectileScript>();

                newProjectileScript.parentTransform.up = transform.up;
                newProjectileScript.parentTransform.right = transform.right;
                newProjectileScript.parentTransform.rotation = transform.rotation;
                newProjectileScript.parentTransform.position = transform.position;
                
                newProjectileScript.maxSpeed = speed;
                newProjectileScript.Box = _box;
                newProjectileScript.sign = sign;
             }
            
            
            yield return new WaitForSeconds(0.2f);
        }
    }
   

    private void Start()
    {
        StartCoroutine(FireProjectile());
    }

   
}
