public class Consts 
{
    public struct PlayerInputs
    {
        public const string HORIZONTAL_INPUT = "Horizontal";
        public const string VERTICAL_INPUT = "Vertical";
    }
    public struct PlayerAnimationParameter
    {
        public const string MOVING = "isMoving";
        public const string CARRYING = "isCarrying";
        public const string CHOPPING = "isChopping";
        public const string MINING = "isMining";
        public const string RECEIVE_ITEM = "ReceiveItem";
    }
    public struct FlatbedMovementPositions
    {
        public const string SPAWN_POSITION = "SpawnPosition";
        public const string STAND_POSITION = "StandPosition";
        public const string EXIT_POSITION = "ExitPosition";
    }
    public struct BuildingAnimationParameter
    {
        public const string UNLOCKED = "isUnlocked";
    }
    public struct StandAnimationParameter
    {
        public const string FLATBED_ARRIVED = "isFlatbedArrived";
    }
    public struct PlayerToolAmount
    {
        public const int HIT_AMOUNT = 1;
    }
    public struct ResourceAnimationParameter
    {
        public const string HIT = "OnHit";
        public const string RESPAWN = "OnRespawn";
        public const string DEAD = "OnDead";
    }
}
