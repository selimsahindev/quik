using UnityEngine;

namespace quik.Demo
{
    public class DemoService : IDemoService
    {
        public DemoService() { }

        public void SayQuik()
        {
            Debug.Log("make it quik.");
        }
    }
}
