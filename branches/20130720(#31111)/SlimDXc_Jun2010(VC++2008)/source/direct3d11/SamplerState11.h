/*
* Copyright (c) 2007-2012 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/
#pragma once

#include "DeviceChild11.h"
#include "SamplerDescription11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class Device;
		
		/// <summary>
		/// A state object defining the behavior of texture sampling. 
		/// </summary>
		/// <unmanaged>ID3D11SamplerState</unmanaged>
		public ref class SamplerState : public DeviceChild
		{
			COMOBJECT(ID3D11SamplerState, SamplerState);

		public:
			/// <summary>
			/// Gets the state's description.
			/// </summary>
			property SamplerDescription Description
			{
				SamplerDescription get();
			}

			/// <summary>
			/// Constructs a new sampler state based on the specified description. If the description
			/// is identical to that of an existing SamplerState object, the existing SamplerState object
			/// is returned instead of a new instance.
			/// </summary>
			/// <param name="device">The device used to create the state.</param>
			/// <param name="description">The state description.</param>
			/// <returns>The SamplerState object.</returns>
			static SamplerState^ FromDescription( Direct3D11::Device^ device, SamplerDescription description );
		};
	}
}