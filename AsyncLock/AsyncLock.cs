using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncLock
{
    public class AsyncLock
    {
        //Instantiate a Singleton of the Semaphore with a value of 1. This means that only 1 thread can be granted access at a time.
        private readonly static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        private int id;

        public AsyncLock(int id)
        {
            this.id = id;
        }

        public async Task TestMehodAsync()
        {
            //Asynchronously wait to enter the Semaphore. If no-one has been granted access to the Semaphore, code execution will proceed, otherwise this thread waits here until the semaphore is released 
            await semaphoreSlim.WaitAsync();
            try
            {
                await Task.Delay(1000);
                Console.WriteLine($"{DateTime.Now.Second}: {id}");
            }
            finally
            {
                //When the task is ready, release the semaphore. It is vital to ALWAYS release the semaphore when we are ready, or else we will end up with a Semaphore that is forever locked.
                //This is why it is important to do the Release within a try...finally clause; program execution may crash or take a different path, this way you are guaranteed execution
                semaphoreSlim.Release();
            }
        }
    }
}
