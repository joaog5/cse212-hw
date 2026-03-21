/// <summary>
/// This queue is circular. When people are added via AddPerson, they are added to the 
/// back of the queue (FIFO rules). When GetNextPerson is called, the next person
/// in the queue is removed and returned, and then may be placed back at the end of the queue.
/// Thus, each person stays in the queue and is given turns.
/// 
/// When a person is added to the queue, a turns parameter is provided to identify how many 
/// turns they will receive. If the turns value is 0 or less, they will stay in the queue forever.
/// If a person runs out of turns, they will not be added back into the queue.
/// </summary>
public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    /// <summary>
    /// Add new people to the queue with a name and number of turns.
    /// </summary>
    /// <param name="name">Name of the person</param>
    /// <param name="turns">Number of turns remaining</param>
    public void AddPerson(string name, int turns)
    {
        // Create a new person and enqueue them at the back (FIFO behavior)
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    /// <summary>
    /// Get the next person in the queue and return them.
    /// The person should go to the back of the queue again unless they have no turns left.
    /// 
    /// A turns value of 0 or less means the person has an infinite number of turns.
    /// An exception is thrown if the queue is empty.
    /// </summary>
    public Person GetNextPerson()
    {
        // Check if the queue is empty → required behavior
        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }
        else
        {
            // Remove the next person from the front of the queue
            Person person = _people.Dequeue();

            // ORIGINAL BUG:
            // The previous implementation only handled (Turns > 1),
            // which caused incorrect behavior for:
            // - Turns == 1 (last turn not handled properly)
            // - Turns <= 0 (infinite turns not handled at all)

            // FIXED LOGIC:

            // Case 1: Infinite turns (0 or negative)
            if (person.Turns <= 0)
            {
                // Do NOT modify the turns value
                // Re-enqueue the person so they stay forever
                _people.Enqueue(person);
            }
            else
            {
                // Case 2: Finite turns → decrement
                person.Turns--;

                // Only re-enqueue if they still have turns remaining
                if (person.Turns > 0)
                {
                    _people.Enqueue(person);
                }
                // If turns == 0 → do NOT re-enqueue (person leaves the queue)
            }

            return person;
        }
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}