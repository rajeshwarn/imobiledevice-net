// <copyright file="MobileImageMounterClientHandle.cs" company="Quamotion">
// Copyright (c) 2016-2018 Quamotion. All rights reserved.
// </copyright>

namespace iMobileDevice.MobileImageMounter
{
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using iMobileDevice.iDevice;
    using iMobileDevice.Lockdown;
    using iMobileDevice.Afc;
    using iMobileDevice.Plist;
    
    
#if !NETSTANDARD1_5
    [System.Security.Permissions.SecurityPermissionAttribute(System.Security.Permissions.SecurityAction.InheritanceDemand, UnmanagedCode=true)]
#endif
#if !NETSTANDARD1_5
    [System.Security.Permissions.SecurityPermissionAttribute(System.Security.Permissions.SecurityAction.Demand, UnmanagedCode=true)]
#endif
    public partial class MobileImageMounterClientHandle : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid
    {
        
        private string creationStackTrace;
        
        private ILibiMobileDevice api;
        
        protected MobileImageMounterClientHandle() : 
                base(true)
        {
            this.creationStackTrace = System.Environment.StackTrace;
        }
        
        protected MobileImageMounterClientHandle(bool ownsHandle) : 
                base(ownsHandle)
        {
            this.creationStackTrace = System.Environment.StackTrace;
        }
        
        public ILibiMobileDevice Api
        {
            get
            {
                return this.api;
            }
            set
            {
                this.api = value;
            }
        }
        
        public static MobileImageMounterClientHandle Zero
        {
            get
            {
                return MobileImageMounterClientHandle.DangerousCreate(System.IntPtr.Zero);
            }
        }
        
#if !NETSTANDARD1_5
        [System.Runtime.ConstrainedExecution.ReliabilityContractAttribute(System.Runtime.ConstrainedExecution.Consistency.WillNotCorruptState, System.Runtime.ConstrainedExecution.Cer.MayFail)]
#endif
        protected override bool ReleaseHandle()
        {
            System.Diagnostics.Debug.WriteLine("Releasing {0} {1} using {2}. This object was created at {3}", this.GetType().Name, this.handle, this.Api, this.creationStackTrace);
            return (this.Api.MobileImageMounter.mobile_image_mounter_free(this.handle) == MobileImageMounterError.Success);
        }
        
        public static MobileImageMounterClientHandle DangerousCreate(System.IntPtr unsafeHandle, bool ownsHandle)
        {
            MobileImageMounterClientHandle safeHandle;
            safeHandle = new MobileImageMounterClientHandle(ownsHandle);
            safeHandle.SetHandle(unsafeHandle);
            return safeHandle;
        }
        
        public static MobileImageMounterClientHandle DangerousCreate(System.IntPtr unsafeHandle)
        {
            return MobileImageMounterClientHandle.DangerousCreate(unsafeHandle, true);
        }
        
        public override string ToString()
        {
            return string.Format("{0} ({1})", this.handle, "MobileImageMounterClientHandle");
        }
        
        public override bool Equals(object obj)
        {
            if (((obj != null) & (obj.GetType() == typeof(MobileImageMounterClientHandle))))
            {
                return ((MobileImageMounterClientHandle)obj).handle.Equals(this.handle);
            }
            else
            {
                return false;
            }
        }
        
        public override int GetHashCode()
        {
            return this.handle.GetHashCode();
        }
        
        /// <summary>
        /// Determines whether two specified instances of <see cref="MobileImageMounterClientHandle"/> are equal.
        /// </summary>
        /// <param name="value1">
        /// The first pointer or handle to compare.
        /// </param>
        /// <param name="value2">
        /// The second pointer or handle to compare.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value1"/> equals <paramref name="value2"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator == (MobileImageMounterClientHandle value1, MobileImageMounterClientHandle value2) 
        {
            return value1.handle == value2.handle;
        }
        
        /// <summary>
        /// Determines whether two specified instances of <see cref="MobileImageMounterClientHandle"/> are not equal.
        /// </summary>
        /// <param name="value1">
        /// The first pointer or handle to compare.
        /// </param>
        /// <param name="value2">
        /// The second pointer or handle to compare.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value1"/> does not equal <paramref name="value2"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator != (MobileImageMounterClientHandle value1, MobileImageMounterClientHandle value2) 
        {
            return value1.handle != value2.handle;
        }
    }
}
