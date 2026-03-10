using System;
using System.Text;

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
namespace ImGuiNET
{
    public static unsafe partial class ImGui
    {
        public static void DockBuilderDockWindow(ReadOnlySpan<char> window_name, uint node_id)
        {
            int utf8WindowNameByteCount = Encoding.UTF8.GetByteCount(window_name);
            byte* utf8WindowNameBytes;
            if (utf8WindowNameByteCount > Util.StackAllocationSizeLimit)
            {
                utf8WindowNameBytes = Util.Allocate(utf8WindowNameByteCount + 1);
            }
            else
            {
                byte* stackPtr = stackalloc byte[utf8WindowNameByteCount + 1];
                utf8WindowNameBytes = stackPtr;
            }

            Util.GetUtf8(window_name, utf8WindowNameBytes, utf8WindowNameByteCount);
            ImGuiNative.igDockBuilderDockWindow(utf8WindowNameBytes, node_id);

            if (utf8WindowNameByteCount > Util.StackAllocationSizeLimit)
            {
                Util.Free(utf8WindowNameBytes);
            }
        }
    }
}
#endif
