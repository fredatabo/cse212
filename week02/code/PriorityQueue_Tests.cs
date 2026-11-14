using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with distinct priorities (low=1, high=5, mid=3),
    //           then dequeue them all.
    // Expected Result: Items come out highest-priority first: high, mid, low.
    // Defect(s) Found: Dequeue loop bound `Count - 1` skipped the last item, and the
    //                  chosen item was never removed, so values repeated / were wrong.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("low", 1);
        priorityQueue.Enqueue("high", 5);
        priorityQueue.Enqueue("mid", 3);

        Assert.AreEqual("high", priorityQueue.Dequeue());
        Assert.AreEqual("mid",  priorityQueue.Dequeue());
        Assert.AreEqual("low",  priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Two items share the highest priority (both 5); verify FIFO tie-break.
    // Expected Result: The first-enqueued of the tied pair ("first") comes out first.
    // Defect(s) Found: Dequeue used `>=`, selecting the LAST tied item instead of the first.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("first", 5);
        priorityQueue.Enqueue("second", 5);
        priorityQueue.Enqueue("low", 1);

        Assert.AreEqual("first",  priorityQueue.Dequeue());
        Assert.AreEqual("second", priorityQueue.Dequeue());
        Assert.AreEqual("low",    priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue a single item, dequeue it, then dequeue from the now-empty queue.
    // Expected Result: InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None — empty-queue check was already correct.
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("only", 1);
        Assert.AreEqual("only", priorityQueue.Dequeue());

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                string.Format("Unexpected exception of type {0} caught: {1}",
                              e.GetType(), e.Message)
            );
        }
    }
}
