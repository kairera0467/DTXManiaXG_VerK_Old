/*
* Copyright (c) 2007-2010 SlimDX Group
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

#include "../ComObject.h"

namespace SlimDX
{
	namespace Direct3D10
	{
		ref class Device;
		ref class StateBlockMask;

		public ref class StateBlock : public ComObject
		{
			COMOBJECT(ID3D10StateBlock, StateBlock);

		public:
			/// <summary>
			/// Gets the device the object is bound to.
			/// </summary>
			property Device^ Device
			{
				SlimDX::Direct3D10::Device^ get();
			}

			/// <summary>
			/// Constructs a new StateBlock object.
			/// </summary>
			/// <param name="device">The device to associate the state block with.</param>
			/// <param name="mask">The mask describing which state properties are relevant to the state block.</param>
			StateBlock( SlimDX::Direct3D10::Device^ device, StateBlockMask mask );

			/// <summary>
			/// Applies the relevant state from the state block.
			/// </summary>
			/// <returns>A result code.</returns>
			Result Apply();
			
			/// <summary>
			/// Captures the relevant state to the state block.
			/// </summary>
			/// <returns>A result code.</returns>
			Result Capture();
			
			/// <summary>
			/// Release all references to device objects.
			/// </summary>
			/// <returns>A result code.</returns>
			Result ReleaseAllDeviceObjects();
		};
	}
}
