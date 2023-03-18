// -------------------------------------------------------------------------------------------------
// <copyright file="LoginComponent.razor.cs" company="RHEA System S.A.">
// 
//   Copyright 2022 RHEA System S.A.
// 
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// 
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Viewer.Components
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Components;

    using ReactiveUI;

    using SysML2.NET.Viewer.ViewModels.Components;

    /// <summary>
    /// Code-behind for the <see cref="LoginComponent"/>
    /// </summary>
    public partial class LoginComponent : IDisposable
    {
        /// <summary>
        /// Gets or sets the <see cref="ILoginViewModel" />
        /// </summary>
        [Inject]
        public ILoginViewModel ViewModel { get; set; }

        /// <summary>
        /// A collection of <see cref="IDisposable" />
        /// </summary>
        private List<IDisposable> disposables = new();

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// Override this method if you will perform an asynchronous operation and
        /// want the component to refresh when that operation is completed.
        /// </summary>
        /// <returns>A <see cref="Task" /> representing any asynchronous operation.</returns>
        protected override Task OnInitializedAsync()
        {
            this.disposables.Add(this.ViewModel.WhenAnyValue(x => x.ErrorMessage)
                .Subscribe(_ => this.InvokeAsync(this.StateHasChanged)));

            this.disposables.Add(this.ViewModel.WhenAnyValue(x => x.AuthenticationStatusKind)
                .Subscribe(_ => this.InvokeAsync(this.StateHasChanged)));

            return base.OnInitializedAsync();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        /// A value indicating whether we are performing cleanup or not
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.disposables.ForEach(x => x.Dispose());
                this.disposables.Clear();
            }
        }

        ~LoginComponent()
        {
            this.Dispose(false);
        }
    }
}
