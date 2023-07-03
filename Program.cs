Console.Write("Penjadwalan CPU\nJumlah Proses: ");
int processesLength = Convert.ToInt32(Console.ReadLine());
string[] processes = new string[processesLength];

Console.Write("Jumlah Burst Time: ");
int burstTimesLength = Convert.ToInt32(Console.ReadLine());
int[] burstTimes = new int[burstTimesLength];
for (int i = 0; i < burstTimesLength; i++)
{
    Console.Write("Burst Time {0}: ", i + 1);
    burstTimes[i] = Convert.ToInt32(Console.ReadLine());
}

string cpuSchedulingOptions;
do
{
    Console.Write("\n1. First-Come First-Served\n2. Shortest Job First Scheduler Non Preemptive\n3. Shortest Job First Scheduler Preemptive\n4. Priority\n5. Round-Robin\nPilih: ");

    cpuSchedulingOptions = Console.ReadLine();
    switch (cpuSchedulingOptions)
    {
        case "1":
            FCFS(processes, burstTimes);
            break;
        case "2":
            SJF_NonPreemptive(processes, burstTimes);
            break;
        case "3":
            SJF_Preemptive(processes, burstTimes);
            break;
        case "4":
            Priority(processes, burstTimes);
            break;
        case "5":
            RoundRobin(processes, burstTimes);
            break;
    }
} while (cpuSchedulingOptions != null || cpuSchedulingOptions != "1" || cpuSchedulingOptions != "2" || cpuSchedulingOptions != "3" || cpuSchedulingOptions != "4" || cpuSchedulingOptions != "5");

static void FCFS(string[] processes, int[] burstTimes)
{
    try
    {
        int[] arrivalTimes = new int[processes.Length];
        arrivalTimes[0] = 0;
        for (int i = 1; i < processes.Length; i++)
        {
            arrivalTimes[i] = arrivalTimes[i - 1] + burstTimes[i - 1];
        }

        int[] turnAroundTimes = new int[processes.Length];
        for (int i = 0; i < processes.Length; i++)
        {
            turnAroundTimes[i] = burstTimes[i] + arrivalTimes[i];
        }

        float totalArrivalTimes = 0;
        for (int i = 0; i < processes.Length; i++)
        {
            totalArrivalTimes += arrivalTimes[i];
        }
        float averageArrivalTimes = totalArrivalTimes / processes.Length;

        Console.WriteLine("\nProcess\tBurst Time\tArrival Time\tTurn Around Time");
        for (int i = 0; i < processes.Length; i++)
        {
            Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}", i + 1, burstTimes[i], arrivalTimes[i], turnAroundTimes[i]);
        }
        Console.WriteLine("\nRata-rata Waktu Tunggu: {0:0.00}", averageArrivalTimes);
    }
    catch (IndexOutOfRangeException)
    {
        Console.WriteLine("\nError: Process > Burst Time.");
        Environment.Exit(0);
    }
}

static void SJF_NonPreemptive(string[] processes, int[] burstTimes)
{
    try
    {
        Array.Sort(processes, (p1, p2) =>
        {
            if (Convert.ToInt32(p1) < processes.Length && Convert.ToInt32(p2) < processes.Length)
            {
                return burstTimes[Convert.ToInt32(p1)] - burstTimes[Convert.ToInt32(p2)];
            }
            else
            {
                return 0;
            }
        });

        int[] arrivalTimes = new int[processes.Length];
        arrivalTimes[0] = 0;
        for (int i = 1; i < processes.Length; i++)
        {
            arrivalTimes[i] = arrivalTimes[i - 1] + burstTimes[i - 1];
        }

        int[] turnAroundTimes = new int[processes.Length];
        for (int i = 0; i < processes.Length; i++)
        {
            turnAroundTimes[i] = burstTimes[i] + arrivalTimes[i];
        }

        float totalArrivalTimes = 0;
        for (int i = 0; i < processes.Length; i++)
        {
            totalArrivalTimes += arrivalTimes[i];
        }
        float averageArrivalTimes = totalArrivalTimes / processes.Length;

        Console.WriteLine("\nProcess\tArrival Time\tBurst Time\tTurn Around Time");
        for (int i = 0; i < processes.Length; i++)
        {
            Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}", i + 1, arrivalTimes[i], burstTimes[i], turnAroundTimes[i]);
        }
        Console.WriteLine("\nRata-rata Waktu Tunggu: {0:0.00}", averageArrivalTimes);
    }
    catch (IndexOutOfRangeException)
    {
        Console.WriteLine("\nError: Process > Burst Time.");
        Environment.Exit(0);
    }
}

static void SJF_Preemptive(string[] processes, int[] burstTimes)
{
    try
    {
        Queue<int> queue = new Queue<int>();
        for (int i = 0; i < processes.Length; i++)
        {
            queue.Enqueue(i);
        }

        int[] arrivalTimes = new int[processes.Length];
        arrivalTimes[0] = 0;

        while (queue.Count != 0)
        {
            int currentProcess = queue.Dequeue();

            arrivalTimes[currentProcess] = arrivalTimes[currentProcess] + burstTimes[currentProcess];

            if (queue.Count != 0)
            {
                int nextProcess = queue.Peek();
                if (burstTimes[nextProcess] < burstTimes[currentProcess])
                {
                    queue.Enqueue(currentProcess);
                    currentProcess = nextProcess;
                }
            }
        }

        float totalArrivalTimes = 0;
        for (int i = 0; i < processes.Length; i++)
        {
            totalArrivalTimes += arrivalTimes[i];
        }
        float averageArrivalTimes = totalArrivalTimes / processes.Length;

        Console.WriteLine("\nProcess\tArrival Time\tBurst Time");
        for (int i = 0; i < processes.Length; i++)
        {
            Console.WriteLine("{0}\t\t{1}\t\t{2}", i + 1, arrivalTimes[i], burstTimes[i]);
        }
        Console.WriteLine("\nRata-rata Waktu Tunggu: {0:0.00}", averageArrivalTimes);
    }
    catch (IndexOutOfRangeException)
    {
        Console.WriteLine("\nError: Process > Burst Time.");
        Environment.Exit(0);
    }
}

static void Priority(string[] processes, int[] burstTimes)
{
    try
    {
        Console.Write("\nJumlah Priority: ");
        int prioritiesLength = Convert.ToInt32(Console.ReadLine());
        int[] priorities = new int[prioritiesLength];
        for (int i = 0; i < priorities.Length; i++)
        {
            Console.Write("Priority {0}: ", i + 1);
            priorities[i] = Convert.ToInt32(Console.ReadLine());
        }

        Queue<int> queue = new Queue<int>();
        for (int i = 0; i < processes.Length; i++)
        {
            queue.Enqueue(i);
        }

        int[] arrivalTimes = new int[processes.Length];
        arrivalTimes[0] = 0;

        while (queue.Count > 0)
        {
            int currentProcess = queue.Dequeue();

            arrivalTimes[currentProcess] = arrivalTimes[currentProcess] + burstTimes[currentProcess];

            if (queue.Count > 0)
            {
                int nextProcess = queue.Peek();
                if (priorities[nextProcess] > priorities[currentProcess])
                {
                    queue.Enqueue(currentProcess);
                    currentProcess = nextProcess;
                }
            }
        }

        float totalArrivalTimes = 0;
        for (int i = 0; i < processes.Length; i++)
        {
            totalArrivalTimes += arrivalTimes[i];
        }
        float averageArrivalTimes = totalArrivalTimes / processes.Length;

        Console.WriteLine("\nProcess\tArrival Time\tBurst Time\tPriority");
        for (int i = 0; i < processes.Length; i++)
        {
            Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}", i + 1, arrivalTimes[i], burstTimes[i], priorities[i]);
        }
        Console.WriteLine("\nRata-rata Waktu Tunggu: {0:0.00}", averageArrivalTimes);
    }
    catch (IndexOutOfRangeException)
    {
        Console.WriteLine("\nError: Process > Burst Time/Priority.");
        Environment.Exit(0);
    }
}

static void RoundRobin(string[] processes, int[] burstTimes)
{
    try
    {
        Console.Write("\nQuantum: ");
        int quantum = Convert.ToInt32(Console.ReadLine());

        Queue<int> queue = new Queue<int>();
        for (int i = 0; i < processes.Length; i++)
        {
            queue.Enqueue(i);
        }

        int[] arrivalTimes = new int[processes.Length];
        arrivalTimes[0] = 0;

        float averageArrivalTimes = 0;

        Console.WriteLine("\nProcess\tArrival Time\tBurst Time");
        while (queue.Count != 0)
        {
            int currentProcess = queue.Dequeue();

            arrivalTimes[currentProcess] = arrivalTimes[currentProcess] + quantum;

            if (burstTimes[currentProcess] <= quantum)
            {
                arrivalTimes[currentProcess] += burstTimes[currentProcess] - quantum;
                Console.WriteLine("{0}\t\t{1}\t\t{2}", currentProcess, arrivalTimes[currentProcess], burstTimes[currentProcess]);
                averageArrivalTimes += arrivalTimes[currentProcess];
            }
            else
            {
                int remainingBurstTime = burstTimes[currentProcess] - quantum;
                arrivalTimes[currentProcess] += quantum;
                Console.WriteLine("{0}\t\t{1}\t\t{2}", currentProcess, arrivalTimes[currentProcess], burstTimes[currentProcess]);
                averageArrivalTimes += arrivalTimes[currentProcess];

                queue.Enqueue(currentProcess);

                while (remainingBurstTime > 0)
                {
                    queue.Dequeue();
                    arrivalTimes[currentProcess] += quantum;
                    Console.WriteLine("{0}\t\t{1}\t\t{2}", currentProcess, arrivalTimes[currentProcess], remainingBurstTime);
                    averageArrivalTimes += arrivalTimes[currentProcess];
                    remainingBurstTime -= quantum;
                }
            }
        }
        averageArrivalTimes /= processes.Length;
        Console.WriteLine("\nRata-rata Waktu Tunggu: {0:0.00}", averageArrivalTimes);
    }
    catch (IndexOutOfRangeException)
    {
        Console.WriteLine("\nError: Process > Burst Time.");
        Environment.Exit(0);
    }
}