using System.Numerics;
using System.Text;

namespace ImGuiNET
{
    public static unsafe partial class ImGui
    {
        private const ImGuiDockNodeFlags DockSpaceNodeFlag = (ImGuiDockNodeFlags)(1 << 10);

        public static uint DockBuilderAddNode(uint node_id)
        {
            return DockBuilderAddNode(node_id, 0);
        }

        public static uint DockBuilderAddNode(uint node_id, ImGuiDockNodeFlags flags)
        {
            return ImGuiNative.igDockBuilderAddNode(node_id, flags);
        }

        public static uint DockBuilderAddDockSpace(uint node_id)
        {
            return DockBuilderAddDockSpace(node_id, 0);
        }

        public static uint DockBuilderAddDockSpace(uint node_id, ImGuiDockNodeFlags flags)
        {
            return ImGuiNative.igDockBuilderAddNode(node_id, flags | DockSpaceNodeFlag);
        }

        public static void DockBuilderRemoveNode(uint node_id)
        {
            ImGuiNative.igDockBuilderRemoveNode(node_id);
        }

        public static void DockBuilderSetNodeSize(uint node_id, Vector2 size)
        {
            ImGuiNative.igDockBuilderSetNodeSize(node_id, size);
        }

        public static uint DockBuilderSplitNode(uint node_id, ImGuiDir split_dir, float size_ratio_for_node_at_dir, out uint out_id_at_dir, out uint out_id_at_opposite_dir)
        {
            fixed (uint* outIdAtDirPtr = &out_id_at_dir)
            fixed (uint* outIdAtOppositeDirPtr = &out_id_at_opposite_dir)
            {
                return ImGuiNative.igDockBuilderSplitNode(node_id, split_dir, size_ratio_for_node_at_dir, outIdAtDirPtr, outIdAtOppositeDirPtr);
            }
        }

        public static void DockBuilderDockWindow(string window_name, uint node_id)
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

        public static void DockBuilderFinish(uint node_id)
        {
            ImGuiNative.igDockBuilderFinish(node_id);
        }
    }
}
