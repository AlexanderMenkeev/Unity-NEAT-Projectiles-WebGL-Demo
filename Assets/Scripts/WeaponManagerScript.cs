using System.Collections.Generic;
using SharpNeat.Genomes.Neat;
using UnityEngine;

public class WeaponManagerScript : MonoBehaviour
{
    public new Camera camera;
    
    public GameObject evolutionAlgorithm;
    public GameObject weaponPrefab;
    
    public List<GameObject> weapons;

    private int _numberOfProjectiles;
    
    private IList<NeatGenome> _genomeList;

    private void InstantiateWeapons()
    {
        Vector2 p11 = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
        Vector2 p00 = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));

        float stepX = (p11.x - p00.x) / (_numberOfProjectiles * 0.5f);
        
        for (int i = 0; i < 4; i++)
        {
            Vector3 pos = new Vector3(p00.x + i * stepX + stepX * 0.5f, p00.y + stepX * 0.8f, 0);
            weapons.Add(Instantiate(weaponPrefab, pos, Quaternion.identity, transform));
        }
        
        for (int i = 0; i < 4; i++)
        {
            Vector3 pos = new Vector3(p00.x + i * stepX + stepX * 0.5f, p11.y - stepX * 0.8f, 0);
            weapons.Add(Instantiate(weaponPrefab, pos, Quaternion.identity, transform));
        }
    }

    private void InitializeWeapons()
    {
        for (int i = 0; i < _numberOfProjectiles; i++)
        {
            WeaponScript weapon = weapons[i].GetComponent<WeaponScript>();
            
            weapon.ProjectileGenome = _genomeList[i];
            weapon.id = _genomeList[i].Id;
            weapon.birthGeneration = _genomeList[i].BirthGeneration;
            weapon.complexity = _genomeList[i].Complexity;
            weapon.isEvaluated = _genomeList[i].EvaluationInfo.IsEvaluated;
            weapon.NodeList = _genomeList[i].NodeList;
            weapon.nodes = _genomeList[i].NodeList.Count;
            
            weapon.Decoder = evolutionAlgorithm.GetComponent<EvolutionAlgorithmScript>().Decoder;
            weapon.parentTransform = transform;
        }
    }
    private void UpdateWeapons()
    {
        for (int i = 0; i < _numberOfProjectiles; i++)
        {
            WeaponScript weapon = weapons[i].GetComponent<WeaponScript>();
            
            weapon.ProjectileGenome = _genomeList[i];
            weapon.id = _genomeList[i].Id;
            weapon.birthGeneration = _genomeList[i].BirthGeneration;
            weapon.complexity = _genomeList[i].Complexity;
            weapon.isEvaluated = _genomeList[i].EvaluationInfo.IsEvaluated;
            weapon.NodeList = _genomeList[i].NodeList;
            weapon.nodes = _genomeList[i].NodeList.Count;
        }
    }
    private void Start()
    {
        camera = Camera.main;
        _numberOfProjectiles = evolutionAlgorithm.GetComponent<EvolutionAlgorithmScript>().populationSize;
        
        InstantiateWeapons();

        _genomeList = evolutionAlgorithm.GetComponent<EvolutionAlgorithmScript>().GenomeList;
        
        InitializeWeapons();
        
        evolutionAlgorithm.GetComponent<EvolutionAlgorithmScript>().NewGenEvent += UpdateWeapons;
    }
    

}
