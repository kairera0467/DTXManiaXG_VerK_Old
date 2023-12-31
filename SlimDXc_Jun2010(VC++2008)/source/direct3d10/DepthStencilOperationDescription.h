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

#include "Enums.h"

namespace SlimDX
{
	namespace Direct3D10
	{	
		public value class DepthStencilOperationDescription : System::IEquatable<DepthStencilOperationDescription>
		{
		private:		
			StencilOperation m_StencilFailOp;
			StencilOperation m_StencilDepthFailOp;
			StencilOperation m_StencilPassOp;
			Direct3D10::Comparison m_StencilFunc;

		internal:
			DepthStencilOperationDescription( const D3D10_DEPTH_STENCILOP_DESC& native );
			
			D3D10_DEPTH_STENCILOP_DESC CreateNativeVersion();
			
		public:
			property StencilOperation FailOperation
			{
				StencilOperation get();
				void set( StencilOperation value );
			}

			property StencilOperation DepthFailOperation
			{
				StencilOperation get();
				void set( StencilOperation value );
			}

			property StencilOperation PassOperation
			{
				StencilOperation get();
				void set( StencilOperation value );
			}

			property Direct3D10::Comparison Comparison
			{
				Direct3D10::Comparison get();
				void set( Direct3D10::Comparison value );
			}

			static bool operator == ( DepthStencilOperationDescription left, DepthStencilOperationDescription right );
			static bool operator != ( DepthStencilOperationDescription left, DepthStencilOperationDescription right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( DepthStencilOperationDescription other );
			static bool Equals( DepthStencilOperationDescription% value1, DepthStencilOperationDescription% value2 );
		};
	}
};