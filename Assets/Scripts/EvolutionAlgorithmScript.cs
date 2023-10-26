using System;
using System.Collections.Generic;
using SharpNeat.Decoders;
using SharpNeat.Decoders.Neat;
using SharpNeat.Genomes.HyperNeat;
using SharpNeat.Genomes.Neat;
using SharpNeat.Network;
using UnityEngine;


public class EvolutionAlgorithmScript : MonoBehaviour
{
    public IList<NeatGenome> GenomeList;

    public event Action NewGenEvent;
    public NeatGenomeDecoder Decoder;
    private CppnGenomeFactory _cppnGenomeFactory;

    
    [Range(1, 10)] public int populationSize;
    private NetworkActivationScheme _activationScheme;

    public uint generation;
    public int inputCount;
    public int outputCount;
    public int cloneOffspringCount;
    public int sexualOffspringCount;
    
    private void InitializeGenomeParams()
    {
        populationSize = 8;
        _activationScheme = NetworkActivationScheme.CreateAcyclicScheme();
        generation = 0;
        inputCount = 3;
        outputCount = 5;
        cloneOffspringCount = 2;
        sexualOffspringCount = 6;
    }
    private void InitializeEvolutionAlgorithm()
    {
        Decoder = new NeatGenomeDecoder(_activationScheme);
        IActivationFunctionLibrary activationFunctionLib = DefaultActivationFunctionLibrary.CreateLibraryCppn();
        NeatGenomeParameters neatGenomeParams = new NeatGenomeParameters();
        neatGenomeParams.InitialInterconnectionsProportion = 0.9;
        neatGenomeParams.AddConnectionMutationProbability = 1.0;
        neatGenomeParams.AddNodeMutationProbability = 1.0;
        neatGenomeParams.ConnectionWeightMutationProbability = 1.0;
        neatGenomeParams.DisjointExcessGenesRecombinedProbability = 0.9;
        neatGenomeParams.NodeAuxStateMutationProbability = 0.2;
        
        _cppnGenomeFactory = new CppnGenomeFactory(inputCount, outputCount, activationFunctionLib, neatGenomeParams);
        GenomeList = _cppnGenomeFactory.CreateGenomeList(populationSize, generation);
    }

    private void Awake()
    {
        InitializeGenomeParams();
        InitializeEvolutionAlgorithm();
    }
    
    
    public void CreateNewGeneration()
    {
        generation++;
        List<NeatGenome> tempList = new List<NeatGenome>();

        foreach (var genome in GenomeList)
        {
            if (genome.EvaluationInfo.IsEvaluated)
                tempList.Add(genome);
        }
        
        if (tempList.Count == 0)
            Debug.Log("You did not choose genomes!");
        else
        {
            for (int i = 0; i < sexualOffspringCount; i++)
                GenomeList[i] = tempList[0].CreateOffspring(tempList[1], generation);

            for (int i = sexualOffspringCount; i < populationSize; i++)
                GenomeList[i] = tempList[i % 2].CreateOffspring(generation);
        
            NewGenEvent?.Invoke();
        }
        
        
    }
    
}
