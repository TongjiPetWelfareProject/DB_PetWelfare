<template>
  <div>
    <el-table ref="tableRef" :data="currentPageData" style="width: 100%;border-radius:10px;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);" @selection-change="handleSelectionChange">
      <el-table-column prop="roomId" label="房间号" width="180%"></el-table-column>
      <el-table-column prop="roomStatus" label="房间情况" width="180%"></el-table-column>
      <el-table-column prop="storey" label="楼层" width="180%"></el-table-column>
      <el-table-column prop="lastCleaningTime" label="上次清理时间" width="180%" sortable
        :sort-method="sortTime"></el-table-column>
      <el-table-column label="操作" width="180">
        <template v-slot="scope">
          <el-button size="mini" type="primary" @click="showEditRoomDialog(scope.row)">完成打扫</el-button>
          <!--
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
          </el-dialog>-->
          <!-- <el-button size="mini" type="danger" @click="deleteRow(scope.row)">删除</el-button> -->
        </template>
      </el-table-column>
    </el-table>
    <br>
    <!--<el-button type="primary" @click="addEmptyRow">添加</el-button>-->
  </div>
  <el-pagination
      v-model:current-page="currentPage"
      v-model:page-size="pageSize"
      :small="small"
      :disabled="disabled"
      :background="background"
      layout="prev, pager, next, jumper"
      :total="tableData.length"
      @size-change="handleSizeChange"
      @current-change="handleCurrentChange"
    />
</template>

<script>
import {  ref, onMounted } from 'vue'
import { ElTable, ElMessageBox, ElButton, ElMessage} from 'element-plus'
import sh_fj_jk from '@/api/sh_fj_jk'

export default {
  components: {
    ElButton,
    ElTable,
  },
  setup() {
    const tableRef = ref(null)
    const tableData = ref([]);
    const currentPage = ref(1);
    const pageSize = ref(15);
    const editRoomDialogVisible = ref(false); // 控制编辑对话框的显示
    const editedRoomStatus = ref(''); // 存储修改后的房间信息
    const editedRoomTime = ref('');


    const handleSelectionChange = (selectedItems) => {
      console.log(selectedItems)
    }

    const sortTime = (a, b) => {
      return new Date(a.cleaningTime) - new Date(b.cleaningTime)
    }

    function getCurrentTime(){
      const now = new Date();
      const currentYear = now.getFullYear();
      const currentMonth = now.getMonth() + 1; // 注意：月份从0开始，需要加1
      const currentDay = now.getDate();
      const currentHours = now.getHours();
      const currentMinutes = now.getMinutes();
      const currentSeconds = now.getSeconds();
    
      const currentTime = `${currentYear}-${currentMonth}-${currentDay} ${currentHours}:${currentMinutes}:${currentSeconds}`;
      return currentTime;
    
    }

    function showEditRoomDialog(row) { // 打开编辑对话框
      //editRoomDialogVisible.value = true;
      const indx = tableData.value.indexOf(row)
      if(indx !== -1){
        const roomId = row.roomId;
        ElMessageBox.confirm(`确认完成打扫${roomId}房间吗`, '提示', {
        type: 'info',
        confirmButtonText: '确认',
        cancelButtonText: '取消',
        }).then(() => {
          sh_fj_jk.sendEditedRoomAPI(roomId) // 调用 API 请求函数更新打扫时间
            .then(response => {
              console.log('信息更新成功', response);
              row.lastCleaningTime = getCurrentTime();
              ElMessage({
                message:'信息更新成功',
                type:'success',
              })
            })
            .catch(error => {
              console.error('信息更新失败', error);
            });
        }).catch(error => {
          console.error('操作已取消', error);
        })
      }
      
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

    const currentPageData = computed(() => {
      const startIndex = (currentPage.value - 1) * pageSize.value;
      const endIndex = startIndex + pageSize.value;
      return tableData.value.slice(startIndex, endIndex);
    })

    const handleSizeChange = newPageSize => {
      pageSize.value = newPageSize;
      currentPage.value = 1;
    }
    
    const handleCurrentChange = newCurrentPage => {
      currentPage.value = newCurrentPage;
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
      currentPageData,
      handleSizeChange,
      handleCurrentChange,
    }
  },
}
</script>
