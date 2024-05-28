using BlazorApp1.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Task = BlazorApp1.Shared.Models.Task;

namespace BlazorApp1.Client.Components
{
    public partial class Modal
    {
        [Parameter]
        public bool Show { get; set; }
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string Body { get; set; }
        [Parameter]
        public string Type { get; set; }
        [Parameter]
        public Person Person { get; set; } = new Person();
        [Parameter]
        public Task Task { get; set; } = new Task();
        [Parameter]
        public Category Category { get; set; } = new Category();
        [Parameter]
        public Category[]? Categories { get; set; }
        [Parameter]
        public Person[]? Persons { get; set; }
        [Parameter]
        public EventCallback<MouseEventArgs> OnCancel { get; set; }
        [Parameter]
        public EventCallback<MouseEventArgs> OnAccept { get; set; }
    }
}
