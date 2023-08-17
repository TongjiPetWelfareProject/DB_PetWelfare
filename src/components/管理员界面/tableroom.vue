<template>
  <div>
    <el-table ref="tableRef" :data="tableData" style="width: 100%;border-radius:10px;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);" @selection-change="handleSelectionChange">
      <el-table-column prop="roomId" label="房间号" width="180%"></el-table-column>
      <el-table-column prop="roomStatus" label="房间情况" width="180%"></el-table-column>
      <el-table-column prop="storey" label="楼层" width="180%"></el-table-column>
      <el-table-column prop="lastCleaningTime" label="上次清理时间" width="180%" sortable
        :sort-method="sortTime"></el-table-column>
      <el-table-column label="操作" width="180">
        <template v-slot="scope">
          <el-button size="mini" type="primary" @click="showEditRoomDialog(scope.row)">编辑</el-button>
          <el-dialog title="编辑房间信息" v-model="editRoomDialogVisible">
            <el-form>
              <el-form-item label="房间情况">
                <el-input v-model="editedRoomStatus" placeholder="输入房间情况" type="textarea"></el-input>
              </el-form-item>
              <el-form-item label="上次清理时间">
               <el-input v-model="editedRoomTime" placeholder="输入上次清理时间" type="time"></el-input>
              </el-form-item>
            </el-form>
            
            <br><br>
            <span slot="footer" class="dialog-footer">
              <el-button @click="editRoomDialogVisible = false">取消</el-button>
              <el-button type="primary" @click="submitEditedRoom">保存</el-button>
            </span>
          </el-dialog>
          <!-- <el-button size="mini" type="danger" @click="deleteRow(scope.row)">删除</el-button> -->
        </template>
      </el-table-column>
    </el-table>
    <br>
    <!--<el-button type="primary" @click="addEmptyRow">添加</el-button>-->
  </div>
</template>

<script>
import {  ref, onMounted } from 'vue'
import { ElTable, ElMessageBox, ElButton } from 'element-plus'
import sh_fj_jk from '@/api/sh_fj_jk'

export default {
  components: {
    ElButton,
    ElTable,
  },
  setup() {
    const tableRef = ref(null)
    const tableData = ref([]);
    const editRoomDialogVisible = ref(false); // 控制编辑对话框的显示
    const editedRoomStatus = ref(''); // 存储修改后的房间信息
    const editedRoomTime = ref('');


    const handleSelectionChange = (selectedItems) => {
      console.log(selectedItems)
    }

    const sortTime = (a, b) => {
      return new Date(a.cleaningTime) - new Date(b.cleaningTime)
    }

    function showEditRoomDialog(index) { // 打开编辑对话框
      editRoomDialogVisible.value = true;
    }

    function submitEditedRoom() {
      sh_fj_jk.sendEditedRoomAPI(compartment, storey, room_status, cleaning_time)
      .then(response => {
        console.log('房间信息更新成功', response);
        editRoomDialogVisible.value = false;
      })
      .catch(error => {
        console.error('更新房间信息失败', error);
      });
      editRoomDialogVisible.value = false;
    }


    onMounted(async () => {
      try {
        const records = await sh_fj_jk.getRoomAPI();
        tableData.value = records;
      } catch (error) {
        console.error('获取房间数据时出错：', error);
      }
    });


    return {
      tableRef,
      tableData,
      handleSelectionChange,
      sortTime,
      showEditRoomDialog,
      submitEditedRoom,
    }
  },
}
</script>
