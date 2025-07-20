using UnityEngine;

namespace quik.Demo
{
    public class DemoService : IDemoService
    {
        public DemoService()
        {
            // Default constructor
        }

        public void SayQuik()
        {
            Debug.Log("make it quik.");
        }
    }
}
