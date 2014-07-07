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
		public value class EffectVariableDescription : System::IEquatable<EffectVariableDescription>
		{
		private:
			System::String^ m_Name;
			System::String^ m_Semantic;
			EffectVariableFlags m_Flags;
			int m_Annotations;
			int m_BufferOffset;
			int m_ExplicitBindPoint;

		internal:
			EffectVariableDescription( const D3D10_EFFECT_VARIABLE_DESC& native );
			
		public:
			property System::String^ Name
			{
				System::String^ get();
			}
			
			property System::String^ Semantic
			{
				System::String^ get();
			}
			
			property EffectVariableFlags Flags
			{
				EffectVariableFlags get();
			}
			
			property int AnnotationCount
			{
				int get();
			}
			
			property int ConstantBufferOffset
			{
				int get();
			}
			
			property int ExplicitBindPoint
			{
				int get();
			}

			static bool operator == ( EffectVariableDescription left, EffectVariableDescription right );
			static bool operator != ( EffectVariableDescription left, EffectVariableDescription right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( EffectVariableDescription other );
			static bool Equals( EffectVariableDescription% value1, EffectVariableDescription% value2 );
		};
	}
};