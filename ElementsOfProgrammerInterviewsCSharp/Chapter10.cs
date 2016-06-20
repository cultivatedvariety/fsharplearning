using System;
using NUnit.Framework;

namespace ElementsOfProgrammerInterviewsCSharp
{
    public class Chapter10
    {

        #region 01 Test if a BinaryTree is balanced

        public static bool _01_IsBalancedTree(BinaryTree binaryTree)
        {
            //a tree is balanced if for each node in the tree, the height of the left and right subtrees differ by at most one
            return _01_IsBalancedNode(binaryTree.Root);
        }

        public static bool _01_IsBalancedNode(BinaryTree.BinaryTreeNode node)
        {
            if (node.Left == null && node.Right == null)
            {
                return true;
            }

            int leftHeight = node.Left != null ? _01_GetNodeHeight(node.Left) : 0;
            int rightHeight = node.Right != null ? _01_GetNodeHeight(node.Right) : 0;
            if (Math.Abs(leftHeight - rightHeight) > 1)
            {
                return false;
            }

            bool leftSubTreeBalanced = node.Left == null || _01_IsBalancedNode(node.Left);
            bool rightSubTreeBalanced = node.Right == null || _01_IsBalancedNode(node.Right);

            return leftSubTreeBalanced && rightSubTreeBalanced;
        }

        public static int _01_GetNodeHeight(BinaryTree.BinaryTreeNode node)
        {
            if (node.Left == null && node.Right == null)
            {
                return 0;
            }

            return 1 + Math.Max(
                node.Left != null ? _01_GetNodeHeight(node.Left) : 0,
                node.Right != null ? _01_GetNodeHeight(node.Right) : 0
                );
        }

        #endregion

        #region BinaryTree

        public class BinaryTree
        {
            public BinaryTreeNode Root { get; }

            public BinaryTree(int rootValue)
            {
                Root = new BinaryTreeNode(rootValue);
            }

            public void Insert(params int[] values)
            {
                foreach (var value in values)
                {
                    var parentNode = GetParentNode(value);

                    if (value <= parentNode.Value)
                    {
                        parentNode.Left = new BinaryTreeNode(value);
                    }
                    else
                    {
                        parentNode.Right = new BinaryTreeNode(value);
                    }
                }
            }

            private BinaryTreeNode GetParentNode(int value)
            {
                BinaryTreeNode parentNode = Root;
                while (true)
                {
                    if (value <= parentNode.Value)
                    {
                        if (parentNode.Left == null)
                        {
                            break;
                        }
                        parentNode = parentNode.Left;
                    }
                    else
                    {
                        if (parentNode.Right == null)
                        {
                            break;
                        }
                        parentNode = parentNode.Right;
                    }
                }
                return parentNode;
            }

            public class BinaryTreeNode
            {
                public BinaryTreeNode(int value)
                {
                    Value = value;
                }

                public int Value { get; }
                public BinaryTreeNode Left { get; set; }
                public BinaryTreeNode Right { get; set; }
            }
        }

        #endregion

    }

    [TestFixture]
    public class Chapter10TestFixture
    {
        #region BinaryTree

        [Test]
        public void When_Inserted_Then_ChildNodesCorrect()
        {
            Chapter10.BinaryTree binaryTree = new Chapter10.BinaryTree(10);
            binaryTree.Insert(5);
            binaryTree.Insert(11);

            Assert.IsNotNull(binaryTree.Root);
            Assert.AreEqual(10, binaryTree.Root.Value);
            Assert.IsNotNull(binaryTree.Root.Left);
            Assert.IsNotNull(binaryTree.Root.Right);
            Assert.AreEqual(5, binaryTree.Root.Left.Value);
            Assert.AreEqual(11, binaryTree.Root.Right.Value);
        }

        #endregion

        #region 01 Test if a BinaryTree is balanced

        [Test]
        public void When_TreeOnlyHasRoot_Then_HeightIsZero()
        {
            Chapter10.BinaryTree binaryTree = new Chapter10.BinaryTree(5);
            Assert.AreEqual(0, Chapter10._01_GetNodeHeight(binaryTree.Root));
        }

        [Test]
        public void When_RootHasOneChild_Then_HeightIsOne()
        {
            Chapter10.BinaryTree binaryTree = new Chapter10.BinaryTree(5);
            binaryTree.Insert(2);

            Assert.AreEqual(1, Chapter10._01_GetNodeHeight(binaryTree.Root));
        }

        [Test]
        public void When_RootHasLeftAndRightChild_Then_HeightIsOne()
        {
            Chapter10.BinaryTree binaryTree = new Chapter10.BinaryTree(5);
            binaryTree.Insert(2);
            binaryTree.Insert(7);

            Assert.AreEqual(1, Chapter10._01_GetNodeHeight(binaryTree.Root));
        }

        [Test]
        public void When_TreeIsUnbalanced_Then_HeightIsMaxOfLeftAndRight()
        {
            Chapter10.BinaryTree binaryTree = new Chapter10.BinaryTree(5);
            binaryTree.Insert(2);
            binaryTree.Insert(7);
            binaryTree.Insert(8);
            binaryTree.Insert(9);

            Assert.AreEqual(3, Chapter10._01_GetNodeHeight(binaryTree.Root));
        }

        [Test]
        public void When_TreeOnlyHasRoot_Then_BalancedIsTrue()
        {
            Chapter10.BinaryTree binaryTree = new Chapter10.BinaryTree(5);
            Assert.IsTrue(Chapter10._01_IsBalancedTree(binaryTree));
        }

        [Test]
        public void When_LeftAndRightSubtreesHaveSameHeight_Then_TreeIsBalanced()
        {
            Chapter10.BinaryTree binaryTree = new Chapter10.BinaryTree(10);
            binaryTree.Insert(5, 7, 3, 15, 12, 17);
            Assert.IsTrue(Chapter10._01_IsBalancedTree(binaryTree));

        }

        [Test]
        public void When_LeftAndRightSubTreeHeightDiffersByOne_Then_TreeIsBalanced()
        {
            Chapter10.BinaryTree binaryTree = new Chapter10.BinaryTree(10);
            binaryTree.Insert(5, 7, 3, 15, 12);
            Assert.IsTrue(Chapter10._01_IsBalancedTree(binaryTree));
        }

        [Test]
        public void When_LeftAndRightSubTreeHeightDiffersByMoreThanOne_Then_TreeIsNotBalanced()
        {
            Chapter10.BinaryTree binaryTree = new Chapter10.BinaryTree(10);
            binaryTree.Insert(5, 4, 3, 2, 1, 15, 12, 17);
            Assert.IsFalse(Chapter10._01_IsBalancedTree(binaryTree));
        }

        #endregion
    }
}