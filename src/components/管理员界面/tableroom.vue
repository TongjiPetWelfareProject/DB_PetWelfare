<template>
  <div>
    <div class="table-container">
      <el-table
  ref="tableRef"
  :data="tableData"
  style="width: 100%; border-radius: 10px; box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);"
  @selection-change="handleSelectionChange"
>
  <el-table-column label="楼层" width="60">
    <template v-slot="scope">
      <span>{{ scope.row.y_name }}</span>
    </template>
  </el-table-column>

  <el-table-column label="房间号" width="1100">
          <template v-slot="scope">
            <div class="room-list">
              <el-tag v-for="room in scope.row.rooms" :key="room.roomId" class="room-item">
                <el-button size="mini" type="text" :class="room.roomStatus === '占用' ? 'occupied-button' : 'transparent-button'"   @click="showEditRoomDialog(room)">{{ room.roomId }}</el-button>
              </el-tag>
            </div>
          </template>
        </el-table-column>
</el-table>
    </div>
    <br />
    <div class="legend-container">
      <span class="legend-block occupied"></span>
      <span class="legend-label">占用 </span>
      <span class="space"></span> <!-- 在占用和空闲之间加一个空格 -->
      <span class="legend-block vacant"></span>
      <span class="legend-label">空闲</span>
    </div>
  </div>

</template>

<script>
import { ref, onMounted } from 'vue';
import { ElTable, ElMessageBox, ElMessage } from 'element-plus';
import sh_fj_jk from '@/api/sh_fj_jk';

export default {
  components: {
    ElTable,
    ElMessageBox,
    ElMessage,
  },
  setup() {
    const tableRef = ref(null);
    const tableData = ref([]);
    const roomInfo=ref('')
    const roomPetID=ref('')

    const handleSelectionChange = (selectedItems) => {
      console.log(selectedItems);
    };

    function getCurrentTime() {
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

    function showEditRoomDialog(room) {
      const roomId = room.roomId;
      const lastCleaningTime = room.lastCleaningTime;
      if(room.roomStatus==='占用') {
        roomInfo.value='当前居住宠物为:'
        // roomPetID.value='1'
        roomPetID.value=fetchRoomPet(room.roomId)
        roomPetID.value+='.'
      }
        ElMessageBox.confirm(`${roomInfo.value}${roomPetID.value}上次打扫时间：${lastCleaningTime}<br>确认完成打扫${roomId}房间吗?`, '提示', {
        type: 'info',
          confirmButtonText: '确认',
          cancelButtonText: '取消',
          dangerouslyUseHTMLString:true,
          center:true
        })
        .then(() => {
              sh_fj_jk.sendEditedRoomAPI(roomId) // 调用 API 请求函数更新打扫时间
                .then(response => {
                  console.log('信息更新成功', response);
                  room.lastCleaningTime = getCurrentTime();
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

            roomInfo.value=''
            roomPetID.value=''
    }

    onMounted(async () => {
      try {
        const records = await sh_fj_jk.getRoomAPI();
        let floorData = [];
        let maxStorey = 0;

        records.forEach((room) => {
          maxStorey = Math.max(maxStorey, room.storey);
        });

        for (let i = 1; i <= maxStorey; i++) {
          const floor = {
            y_name: i.toString(),
            rooms: [],
          };
          
          records.forEach((room) => {
            if (room.storey === i) {
              floor.rooms.push(room);
            }
          });

          floorData.push(floor);
        }

        tableData.value = floorData;
      } catch (error) {
        console.error('获取房间数据时出错：', error);
      }
    });

    return {
      tableRef,
      tableData,
      handleSelectionChange,
      showEditRoomDialog,
    };
  },
};
</script>

<style scoped>
.table-container {
  display: flex;
  justify-content: space-between;
  flex-direction: row;
}
.room-list {
  display: inline-block;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  font-size: 66px; /* 修改行间距 */
}
.room-item {
  margin-right: 14px; /* 修改房间号间距 */
  font-size: 16px; /* 修改字号 */
  background-color: transparent;
  border: none;
}

.transparent-button {
  position: relative;
  font-size: 12px;
  width: 40px;
  height: 30px; /* 将按钮高度增加以容纳长方形底边 */
  overflow: hidden;
  padding: 0;
  background-color: transparent;
  border: none;

  /* 新增样式 */
  display: flex; /* 使用flex布局 */
  flex-direction: column; /* 垂直方向布局 */
}

.transparent-button::before,
.transparent-button::after,
.bottom-rect {
  position: absolute;
  content: '';
}

.transparent-button::before {
  top: 0;
  left: 0;
  width: 0;
  height: 0;
  border-left: 20px solid transparent;
  border-right: 22px solid transparent;
  border-bottom: 10px solid rgba(160, 212, 247, 0.4); /* 将三角形高度减小一点 */
}

.transparent-button::after {
  bottom: -5px; /* 将底边上移以与三角形对齐 */
  left: 3px;
  width: 35px;
  height: 25px; /* 将底边高度减小一点 */
  background-color: rgba(160, 212, 247, 0.4);
  border-radius: 0;
}

.bottom-rect {
  bottom: 0;
  left: -30px; /* 将长方形左移以与三角形对齐 */
  width: 100px; /* 设置长方形宽度 */
  height: 15px; /* 设置长方形高度 */
  background-color: rgba(160, 212, 247, 0.4);
}

.occupied-button {
  position: relative;
  font-size: 12px;
  width: 40px;
  height: 30px; /* 将按钮高度增加以容纳长方形底边 */
  overflow: hidden;
  padding: 0;
  background-color: transparent;
  border: none;

  /* 新增样式 */
  display: flex; /* 使用flex布局 */
  flex-direction: column; /* 垂直方向布局 */
}

.occupied-button::before,
.occupied-button::after,
.bottom-rect {
  position: absolute;
  content: '';
}

.occupied-button::before {
  top: 0;
  left: 0;
  width: 0;
  height: 0;
  border-left: 20px solid transparent;
  border-right: 22px solid transparent;
  border-bottom: 10px solid rgba(231, 91, 119, 0.4); /* 将三角形高度减小一点 */
}

.occupied-button::after {
  bottom: -5px; /* 将底边上移以与三角形对齐 */
  left: 3px;
  width: 35px;
  height: 25px; /* 将底边高度减小一点 */
  background-color: rgba(231, 91, 119, 0.4);
  border-radius: 0;
}

.bottom-rect {
  bottom: 0;
  left: -30px; /* 将长方形左移以与三角形对齐 */
  width: 100px; /* 设置长方形宽度 */
  height: 15px; /* 设置长方形高度 */
  background-color: rgba(231, 91, 119, 0.4);
}

.legend-container {
  display: flex;
  align-items: center;
  margin-top: 10px;
}

.legend-block {
  width: 12px;
  height: 12px;
  display: inline-block;
  margin-right: 5px;
}

.legend-block.occupied {
  background-color: rgba(231, 91, 119, 0.4);
}

.legend-block.vacant {
  background-color: rgba(160, 212, 247, 0.4);
}

.legend-label {
  font-size: 14px;
}
.space {
  margin-right: 10px;
}

</style>
