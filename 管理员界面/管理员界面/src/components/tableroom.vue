<template>
  <div>
    <el-table ref="tableRef" :data="tableData" style="width: 100%;border-radius:10px;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);" @selection-change="handleSelectionChange">
      <el-table-column prop="roomId" label="房间ID" width="180"></el-table-column>
      <el-table-column prop="petId" label="宠物ID" width="180"></el-table-column>
      <el-table-column prop="floor" label="楼层" width="180"></el-table-column>
      <el-table-column prop="lastCleaningTime" label="上次清理时间" width="150" sortable
        :sort-method="sortTime"></el-table-column>
      <el-table-column label="操作" width="180">
        <template v-slot="scope">
          <el-button size="mini" type="primary" @click="editRow(scope.row)">编辑</el-button>
          <el-button size="mini" type="danger" @click="deleteRow(scope.row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    <br>
    <el-button type="primary" @click="addEmptyRow">添加</el-button>
  </div>
</template>

<script>
import { ref } from 'vue'
import { ElTable, ElMessageBox, ElButton } from 'element-plus'

export default {
  components: {
    ElButton,
    ElTable,
  },
  setup() {
    const tableRef = ref(null)
    const tableData = ref([
      { roomId: "001", petId: '1001', floor: '1', lastCleaningTime: '2021-10-01' },
      { roomId: "002", petId: '1002', floor: '2', lastCleaningTime: '2021-10-02' },
      { roomId: "003", petId: '空闲', floor: '3', lastCleaningTime: '2021-10-03' },
      { roomId: "004", petId: '1004', floor: '4', lastCleaningTime: '2021-10-04' },
      { roomId: "005", petId: '1005', floor: '5', lastCleaningTime: '2021-10-05' },
    ])

    const handleSelectionChange = (selectedItems) => {
      console.log(selectedItems)
    }

    const sortTime = (a, b) => {
      return new Date(a.lastCleaningTime) - new Date(b.lastCleaningTime)
    }

    const deleteRow = (row) => {
      ElMessageBox.confirm('确定删除该房间吗？', '提示', {
        type: 'warning',
        confirmButtonText: '确定',
        cancelButtonText: '取消',
      }).then(() => {
        const index = tableData.value.indexOf(row)
        if (index !== -1) {
          tableData.value.splice(index, 1)
        }
      }).catch(() => {
        // 取消删除
      })
    }

    const addEmptyRow = () => {
      const emptyRow = {
        roomId: '',
        petId: '',
        floor: '',
        lastCleaningTime: '',
      }
      tableData.value.push(emptyRow)
    }

    const editRow = (row) => {
      console.log('编辑行', row)
    }

    return {
      tableRef,
      tableData,
      handleSelectionChange,
      sortTime,
      deleteRow,
      addEmptyRow,
      editRow,
    }
  },
}
</script>