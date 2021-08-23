namespace _02.Two_Three
{
    using System;
    using System.Text;

    public class TwoThreeTree<T> where T : IComparable<T>
    {
        private TreeNode<T> root;

        public void Insert(T key)
        {
            if (this.root == null)
            {
                this.root = new TreeNode<T>(key);
                return;
            }

            var leaf = this.FindNode(this.root, key);

            if (leaf.IsTwoNode() || leaf.RightKey.Equals(default(T)))
            {
                if (key.CompareTo(leaf.LeftKey) < 0)
                {
                    leaf.RightKey = leaf.LeftKey;
                    leaf.LeftKey = key;
                }
                else
                {
                    leaf.RightKey = key;
                }
            }
            else if (leaf.IsThreeNode())
            {
                this.Split(leaf, key);
            }
        }

        private void Split(TreeNode<T> node, T key)
        {
            T median = this.ChooseMedian(node, key);

            var leftNode = new TreeNode<T>(node.LeftKey);
            var rightNode = new TreeNode<T>(node.RightKey);

            if (this.root == node)
            {
                var newRoot = new TreeNode<T>(median);
                newRoot.LeftChild = leftNode;
                newRoot.RightChild = rightNode;
                if (this.root.LeftChild != null)
                {
                    newRoot.LeftChild.LeftChild = this.root.LeftChild;
                }
                if (this.root.MiddleChild != null)
                {
                    newRoot.LeftChild.RightChild = this.root.MiddleChild;
                }
                if (this.root.RightChild != null)
                {
                    newRoot.RightChild.LeftChild = this.root.RightChild;
                }

                if (this.root.HelperChild != null)
                {
                    if (this.root.HelperChild.LeftKey.CompareTo(this.root.RightChild.LeftKey) > 0)
                    {
                        newRoot.RightChild.RightChild = this.root.HelperChild;
                        newRoot.RightChild.RightChild.Parent = newRoot.RightChild;
                    }
                    else if (this.root.HelperChild.LeftKey.CompareTo(this.root.LeftChild.LeftKey) > 0 && this.root.HelperChild.LeftKey.CompareTo(this.root.RightChild.LeftKey) < 0)
                    {
                        newRoot.RightChild.LeftChild = this.root.HelperChild;
                        newRoot.RightChild.LeftChild.Parent = newRoot.RightChild;
                    }
                    else
                    {
                        newRoot.LeftChild.LeftChild = this.root.HelperChild;
                        newRoot.LeftChild.LeftChild.Parent = this.root.LeftChild;
                    }
                }
                

                //set parents
                newRoot.LeftChild.Parent = newRoot;
                newRoot.RightChild.Parent = newRoot;
                this.root = newRoot;
            }
            else
            {
                var parent = node.Parent;
                if (parent.IsTwoNode() || parent.RightKey.Equals(default(T)))
                {
                    if (median.CompareTo(parent.LeftKey) < 0)
                    {
                        parent.RightKey = parent.LeftKey;
                        parent.LeftKey = median;

                        parent.LeftChild = leftNode;
                        parent.MiddleChild = rightNode;

                        //set parents
                        parent.LeftChild.Parent = parent;
                        parent.MiddleChild.Parent = parent;
                    }
                    else
                    {
                        parent.RightKey = median;

                        parent.RightChild = rightNode;
                        parent.MiddleChild = leftNode;

                        //set parents
                        parent.RightChild.Parent = parent;
                        parent.MiddleChild.Parent = parent;
                    }
                }
                else if (parent.IsThreeNode())
                {
                    //right
                    if (parent.RightChild.LeftKey.Equals(leftNode.LeftKey))
                    {
                        parent.HelperChild = new TreeNode<T>(rightNode.LeftKey);
                        parent.RightChild.RightKey = default(T);
                    }
                    //left
                    else if (parent.LeftChild.LeftKey.Equals(leftNode.LeftKey))
                    {
                        parent.HelperChild = new TreeNode<T>(leftNode.LeftKey);
                        parent.LeftChild.RightKey = default(T);
                    }
                    //middle
                    else if (parent.MiddleChild.RightKey.Equals(rightNode.LeftKey))
                    {
                        parent.HelperChild = new TreeNode<T>(rightNode.LeftKey);
                        parent.MiddleChild.RightKey = default(T);
                    }

                    this.Split(node.Parent, median);
                }
                //put median in the parent
                //check if parent must split too
            }
        }

        private TreeNode<T> FindNode(TreeNode<T> root, T key)
        {
            if (root.IsLeaf())
            {
                return root;
            }

            if (key.CompareTo(root.LeftKey) <= 0)
            {
                return this.FindNode(root.LeftChild, key);
            }
            else if (key.CompareTo(root.LeftKey) > 0 && key.CompareTo(root.RightKey) < 0)
            {
                return this.FindNode(root.MiddleChild, key);
            }
            else
            {
                return this.FindNode(root.RightChild, key);
            }
        }



        private T ChooseMedian(TreeNode<T> leaf, T key)
        {
            T median = default(T);
            if (key.CompareTo(leaf.LeftKey) < 0)
            {
                median = leaf.LeftKey;
                leaf.LeftKey = key;
            }
            else if (key.CompareTo(leaf.LeftKey) > 0 && key.CompareTo(leaf.RightKey) < 0)
            {
                median = key;
            }
            else
            {
                median = leaf.RightKey;
                leaf.RightKey = key;
            }

            return median;
        }


        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            RecursivePrint(this.root, sb);
            return sb.ToString();
        }

        private void RecursivePrint(TreeNode<T> node, StringBuilder sb)
        {
            if (node == null)
            {
                return;
            }

            if (node.LeftKey != null)
            {
                sb.Append(node.LeftKey).Append(" ");
            }

            if (node.RightKey != null)
            {
                sb.Append(node.RightKey).Append(Environment.NewLine);
            }
            else
            {
                sb.Append(Environment.NewLine);
            }

            if (node.IsTwoNode())
            {
                RecursivePrint(node.LeftChild, sb);
                RecursivePrint(node.RightChild, sb);
            }
            else if (node.IsThreeNode())
            {
                RecursivePrint(node.LeftChild, sb);
                RecursivePrint(node.MiddleChild, sb);
                RecursivePrint(node.RightChild, sb);
            }
        }
    }
}
