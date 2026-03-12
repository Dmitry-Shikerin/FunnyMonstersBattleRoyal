namespace Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Enums
{
    public enum SoundPlayMode
    {
        /// <summary> Sounds are played randomly from a sounds list and refilled after all have been played. This uses true no-repeat, so even when all the sounds in the list have been played, it will not play the previous sound again on the next pass </summary>
        Random = 0,
        /// <summary> Sounds are played in the order they have been added to the sounds list. This option has additional settings </summary>
        Sequence = 1
    }
}