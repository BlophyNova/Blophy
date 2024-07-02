using UtilityCode.Singleton;
namespace Manager
{
    public class JudgeManager : MonoBehaviourSingleton<JudgeManager>
    {
        public static float perfect = .06f;//完美判定±60ms
        public static float good = .10f;//Good判定±100ms
        public static float bad = .16f;//Bad判定±160ms
    }
}
