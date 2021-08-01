namespace _02.DOM
{
    using System;
    using System.Collections.Generic;
    using _02.DOM.Interfaces;
    using _02.DOM.Models;

    public class DocumentObjectModel : IDocument
    {
        public DocumentObjectModel(IHtmlElement root)
        {
            this.Root = root;
        }

        public DocumentObjectModel()
        {
            this.Root = new HtmlElement(ElementType.Document,
                                            new HtmlElement(ElementType.Html,
                                                new HtmlElement(ElementType.Head),
                                                new HtmlElement(ElementType.Body)));
        }

        public IHtmlElement Root { get; private set; }

        public IHtmlElement GetElementByType(ElementType type)
        {
            var queue = new Queue<IHtmlElement>();
            queue.Enqueue(this.Root);

            while (queue.Count != 0)
            {
                var element = queue.Dequeue();
                if (element.Type == type)
                {
                    return element;
                }
                foreach (var child in element.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        public List<IHtmlElement> GetElementsByType(ElementType type)
        {
            var result = new List<IHtmlElement>();
            this.GetElementsByTypeDFS(this.Root, type, result);
            return result;
        }

        public bool Contains(IHtmlElement htmlElement)
        {
            var queue = new Queue<IHtmlElement>();
            queue.Enqueue(this.Root);

            while (queue.Count != 0)
            {
                var element = queue.Dequeue();
                if (element.Equals(htmlElement))
                {
                    return true;
                }
                foreach (var child in element.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return false;
        }

        public void InsertFirst(IHtmlElement parent, IHtmlElement child)
        {
            var element = this.FindElement(parent);
            if (element == null)
            {
                throw new InvalidOperationException();
            }

            child.Parent = element;
            var newChildren = new List<IHtmlElement>();
            var childrenCount = element.Children.Count;

            for (int i = 0; i < childrenCount; i++)
            {
                newChildren.Add(element.Children[i]);
            }
            element.Children.RemoveAll(x => 2 == 2);

            parent.Children.Add(child);
            for (int i = 0; i < newChildren.Count; i++)
            {
                parent.Children.Add(newChildren[i]);
            }
        }

        public void InsertLast(IHtmlElement parent, IHtmlElement child)
        {
            var element = this.FindElement(parent);
            if (element == null)
            {
                throw new InvalidOperationException();
            }

            child.Parent = parent;
            element.Children.Add(child);
        }

        public void Remove(IHtmlElement htmlElement)
        {
            var element = this.FindElement(htmlElement);
            if (element == null)
            {
                throw new InvalidOperationException();
            }

            var parentElement = element.Parent;
            parentElement.Children.Remove(element);
        }

        public void RemoveAll(ElementType elementType)
        {
            var queue = new Queue<IHtmlElement>();
            queue.Enqueue(this.Root);

            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                if (current.Type == elementType)
                {
                    var parent = current.Parent;
                    parent.Children.Remove(current);
                }
                foreach (var item in current.Children)
                {
                    queue.Enqueue(item);
                }
            }
        }

        public bool AddAttribute(string attrKey, string attrValue, IHtmlElement htmlElement)
        {
            var element = this.FindElement(htmlElement);
            if (element == null)
            {
                throw new InvalidOperationException();
            }

            if (!element.Attributes.ContainsKey(attrKey))
            {
                element.Attributes.Add(attrKey, attrValue);
                return true;
            }

            return false;
        }

        public bool RemoveAttribute(string attrKey, IHtmlElement htmlElement)
        {
            var element = this.FindElement(htmlElement);
            if (element == null)
            {
                throw new InvalidOperationException();
            }

            if (!element.Attributes.ContainsKey(attrKey))
            {
                return false;
            }

            element.Attributes.Remove(attrKey);
            return true;
        }

        public IHtmlElement GetElementById(string idValue)
        {
            var queue = new Queue<IHtmlElement>();
            queue.Enqueue(this.Root);

            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                if (current.Attributes.ContainsKey("id"))
                {
                    if (current.Attributes.TryGetValue("id", out string value))
                    {
                        if (value == idValue)
                        {
                            return current;
                        }
                    }
                }
                foreach (var item in current.Children)
                {
                    queue.Enqueue(item);
                }
            }

            return null;
        }

        public override string ToString()
        {
            var result = new List<string>();
            this.ToStringDFS(result, this.Root, 0);

            return String.Join("", result);
        }

        private void ToStringDFS(List<string> result, IHtmlElement root, int spaces)
        {
            result.Add(new string(' ', spaces) + root.Type + "\r\n");

            foreach (var item in root.Children)
            {
                this.ToStringDFS(result, item, spaces + 2);
            }
        }

        private void GetElementsByTypeDFS(IHtmlElement root, ElementType type, List<IHtmlElement> result)
        {
            foreach (var child in root.Children)
            {
                this.GetElementsByTypeDFS(child, type, result);
            }

            if (root.Type == type)
            {
                result.Add(root);
            }
        }

        private IHtmlElement FindElement(IHtmlElement element)
        {
            var queue = new Queue<IHtmlElement>();
            queue.Enqueue(this.Root);

            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                if (current.Equals(element))
                {
                    return current;
                }
                foreach (var item in current.Children)
                {
                    queue.Enqueue(item);
                }
            }

            return null;
        }
    }
}
