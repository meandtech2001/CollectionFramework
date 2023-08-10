using ConceptArchitect.Collections;
using System;
using Xunit;

namespace CollectionTests
{
    public class FixedStackSpecs
    {
        FixedStack<int> intStack;
        int size = 3;

        public FixedStackSpecs()
        {
            intStack = new FixedStack<int>(size);
        }

        private void AssertFailed(string reason = "Not Yet Implemented")
        {
            Assert.True(false, reason);
        }

        [Fact(
                //Skip = "Not Yet Implemented"
        )]
        public void StackShouldBeInitializedWithSize()
        {
            var stack = new FixedStack<int>(10);
            Assert.NotNull(stack);
        }
        [Fact(
               // Skip = "Not Yet Implemented"
        )]
        public void StackShouldBeInitiallyEmpty()
        {
            Assert.True(intStack.IsEmpty);
        }

        [Fact(
        // Skip = "Not Yet Implemented"
        )]
        public void PushPushesNumberToStack()
        {
            intStack.Push(20);

            Assert.Equal(1, intStack.Length);
        }


        [Fact(
               // Skip = "Not Yet Implemented"
        )]
        public void PushingAnItemToAnEmptyStackMakesItNonEmpty()
        {
            intStack.Push(20);

            Assert.False(intStack.IsEmpty);
        }


        [Fact(
               // Skip = "Not Yet Implemented"
        )]
        public void PushingItemsEqualsToTheSizeOfStackMakesItFull()
        {
            for (int i = 0; i < size; i++)
                intStack.Push(i);


            Assert.True(intStack.IsFull);
        }

        [Fact(
               // Skip = "Not Yet Implemented"
        )]
        public void PushingAnItemToAFullStackFails()
        {
            //Arrange --- Get a Full Stack
            for(var i = 0; i < size; i++)
            {
                intStack.Push(i);
            }

            //Act+Assert

            Assert.Throws<StackOverflowException>(()=> intStack.Push(10)); //This should fail
        }

        [Fact(
                //Skip = "Not Yet Implemented"
        )]
        public void PoppingAnItemFromAnEmptyStackShouldFail()
        {
            Assert.Throws<EmptyContainerException>(() => intStack.Pop());

        }


        [Fact(
               // Skip = "Not Yet Implemented"
        )]
        public void PoppingFromAFullStackMakesItNonFull()
        {
            //Arrange 

           for(int i = 0; i < size; i++)
            {
                intStack.Push(i);
            }

            //Act
             intStack.Pop();

            //Assert

            Assert.False(intStack.IsFull);



        }

        [Fact(
                //Skip = "Not Yet Implemented"
        )]
        public void PoppingReturnsTheLastItemPushed()
        {
            //Arrange
            int elementToPush = 10;
            intStack.Push(elementToPush);
            
            //Act
            int lastElement = intStack.Pop();

            //Assert
            Assert.Equal(elementToPush, lastElement) ;
        }


        [Fact(
               /// Skip = "Not Yet Implemented"
        )]
        public void ClearingAStackMakesItEmpty()
        {
            //Arrange
            intStack.Push(1);

            //Act
            intStack.Clear();

            //Assert
            Assert.True(intStack.IsEmpty);

        }

        [Fact(
               // Skip = "Not Yet Implemented"
        )]
        public void PeekingInAnEmptyStackFails()
        {
            bool success;

            Assert.Throws<EmptyContainerException>( ()=>intStack.Peek());
        }

        [Fact(
               // Skip = "Not Yet Implemented"
        )]
        public void PeekingInAnNonEmptyStackReturnsLastItemPushed()
        {
            //Arrange
            int item=10;
            intStack.Push(item);

            //ACT
            int value = intStack.Peek();

            //Assert
            Assert.Equal(item, value);
        }

        [Fact]
        public void PopShouldPopItemsInLastInFirstOutOrder()
        {
            int[] values = { 5, 10, 6 };
            foreach(var item in values)
            {
                intStack.Push(item);
            }
            for(var i=0; i<values.Length; i++)
            {
                int value = intStack.Pop();
                Assert.Equal(values[values.Length-1-i], value);
            }
            

            
        }




    }
}