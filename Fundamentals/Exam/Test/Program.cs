using _01._BrowserHistory;
using _02.DOM;
using _02.DOM.Models;
using System;
using System.Threading.Channels;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var ulElement = new HtmlElement(ElementType.UnorderedList,
                           new HtmlElement(ElementType.ListItem),
                           new HtmlElement(ElementType.ListItem),
                           new HtmlElement(ElementType.ListItem)
                       );

            var document = new DocumentObjectModel(
                new HtmlElement(ElementType.Document,
                    new HtmlElement(ElementType.Html,
                        new HtmlElement(ElementType.Head),
                        new HtmlElement(ElementType.Body,
                            new HtmlElement(ElementType.Paragraph),
                            new HtmlElement(ElementType.Div),
                            new HtmlElement(ElementType.Anchor,
                            ulElement
                        )
                    )
                )
            ));

            var h1Element = new HtmlElement(ElementType.H1);

            document.InsertFirst(ulElement, h1Element);

            //Assert.AreEqual(4, this._ulElement.Children.Count);
            //Assert.AreEqual(h1Element, this._ulElement.Children[0]);
            //Assert.AreEqual(this._ulElement, h1Element.Parent);
        }
    }
}
