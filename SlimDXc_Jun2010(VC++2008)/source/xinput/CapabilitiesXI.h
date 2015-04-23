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
#include "Gamepad.h"
#include "Vibration.h"

namespace SlimDX
{
	namespace XInput
	{
		/// <summary>
		/// Describes the capabilities of a controller.
		/// </summary>
		/// <unmanaged>XINPUT_CAPABILITIES</unmanaged>
		public value class Capabilities : System::IEquatable<Capabilities>
		{
		private:
			DeviceType m_Type;
			DeviceSubtype m_SubType;
			CapabilityFlags m_Flags;
			XInput::Gamepad m_Gamepad;
			XInput::Vibration m_Vibration;
		
		internal:
			Capabilities( const XINPUT_CAPABILITIES& native );
			
		public:
			/// <summary>
			/// Gets the controller type.
			/// </summary>
			property DeviceType Type
			{
				DeviceType get();
			}

			/// <summary>
			/// Gets the controller subtype.
			/// </summary>
			property DeviceSubtype Subtype
			{
				DeviceSubtype get();
			}

			/// <summary>
			/// Gets the features of the controller. 
			/// </summary>
			property CapabilityFlags Flags
			{
				CapabilityFlags get();
			}

			/// <summary>
			/// Gets a <see cref="XInput::Gamepad"/> structure describing available gamepad features.
			/// </summary>
			property XInput::Gamepad Gamepad
			{
				XInput::Gamepad get();
			}

			/// <summary>
			/// Gets a <see cref="XInput::Vibration"/> structure describing available vibration features.
			/// </summary>
			property XInput::Vibration Vibration
			{
				XInput::Vibration get();
			}

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( Capabilities left, Capabilities right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( Capabilities left, Capabilities right );

			/// <summary>
			/// Returns the hash code for this instance.
			/// </summary>
			/// <returns>A 32-bit signed integer hash code.</returns>
			virtual int GetHashCode() override;

			/// <summary>
			/// Returns a value that indicates whether the current instance is equal to a specified object. 
			/// </summary>
			/// <param name="obj">Object to make the comparison with.</param>
			/// <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
			virtual bool Equals( System::Object^ obj ) override;

			/// <summary>
			/// Returns a value that indicates whether the current instance is equal to the specified object. 
			/// </summary>
			/// <param name="other">Object to make the comparison with.</param>
			/// <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
			virtual bool Equals( Capabilities other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( Capabilities% value1, Capabilities% value2 );
		};
	}
}