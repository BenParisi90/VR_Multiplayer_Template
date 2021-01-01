using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleSetType
{
    Count
}

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;

    public static List<ParticleSetType> particleSetTypes = new List<ParticleSetType>{

    };

    public List<ParticleSet> particleSets;

    void Start()
    {
        instance = this;
    }

    public void PlayParticleSet(ParticleSetType type, Vector3 position, int playerNum = -1)
    {
        int particleSetIndex = particleSetTypes.IndexOf(type);
        ParticleSet targetParticleSet = particleSets[particleSetIndex];   
        targetParticleSet.root.position = position;
        foreach(ParticleSystem particleSystem in targetParticleSet.particleSystems)
        {
            particleSystem.Play();
        }
    }
}

[System.Serializable]
public class ParticleSet
{
    public Transform root;
    public List<ParticleSystem> particleSystems;
}