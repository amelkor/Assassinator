namespace Game.Character
{
    public interface IAnimatedCharacter
    {
        public ICharacterGroundDetection Ground { get; }
        public ICharacterMotion Motion { get; }
        public ICharacterRotation Rotation { get; }
        public ICharacterInput Input { get; }
    }
}