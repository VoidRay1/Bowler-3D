using UnityEngine;

[CreateAssetMenu]
public class SoundInteractableFactory : ScriptableObject
{
    [SerializeField] private AudioClip _metalGlassSound;
    [SerializeField] private AudioClip _glassGlassSound;
    [SerializeField] private AudioClip _metalMetalSound;
    [SerializeField] private AudioClip _metalWoodSound;

    public AudioClip GetAudioClipByInteraction(MaterialType materialType1, MaterialType materialType2)
    {
        if((materialType1 == MaterialType.Metal && materialType2 == MaterialType.Glass) 
            || (materialType1 == MaterialType.Glass && materialType2 == MaterialType.Metal))
        {
            return _metalGlassSound;
        }
        else if(materialType1 == MaterialType.Glass && materialType2 == MaterialType.Glass)
        {
            return _glassGlassSound;
        }
        else if(materialType1 == MaterialType.Metal && materialType2 == MaterialType.Metal)
        {
            return _metalMetalSound;
        }
        else if(materialType1 == MaterialType.Metal && materialType2 == MaterialType.Wood
             || (materialType1 == MaterialType.Wood && materialType2 == MaterialType.Metal))
        {
            return _metalWoodSound;
        }
        return null;
    }
}