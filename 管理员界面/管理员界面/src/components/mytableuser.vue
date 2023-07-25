<template>
      <el-table :data="tableData" height="600" style="width: 84vw;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);border-radius: 10px; header-cell-style:align= 'center';"  >
        <el-table-column label="用户ID" prop="id" width=100></el-table-column>
        <el-table-column label="用户名" prop="username" width=120></el-table-column>
        <el-table-column label="电话" prop="phone" width="140"></el-table-column>
        <el-table-column label="地址" prop="address" width="160"></el-table-column>
        <el-table-column label="操作" width="360">
          <template #default="scope">
            <el-button size="mini" type="danger" @click="handleBan(scope.row)">禁言</el-button>
            <el-button size="mini" type="danger" @click="handleBlock(scope.row)">封号</el-button>
          </template>
        </el-table-column>
      </el-table>
    </template>
    
    <script setup>
    import { ref } from 'vue';
    
    const tableData = ref([
      {
        id: "00001",
        username: '张三',
        phone: '123-456-7890',
        address: '同济大学四平路校区',
      },
      {
        id: "00002",
        username: '李四',
        phone: '987-654-3210',
        address: '同济大学嘉定校区',
      },
      {
        id: "00003",
        username: '王五',
        phone: '123-456-7890',
        address: '同济大学四平路校区',
      },
      {
        id: "00004",
        username: '张三',
        phone: '987-654-3210',
        address: '同济大学嘉定校区',
      }, {
        id: "00005",
        username: '李四',
        phone: '123-456-7890',
        address: '同济大学四平路校区',
      },
      {
        id: "00006",
        username: '王五',
        phone: '987-654-3210',
        address: '同济大学嘉定校区',
      },
      {
        id: "00007",
        username: '张三',
        phone: '987-654-3210',
        address: '同济大学四平路校区',
      },
      {
        id: "00008",
        username: '李四',
        phone: '987-654-3210',
        address: '同济大学嘉定校区',
      },
    ]);
    
    const handleBan = (row) => {
      console.log('Ban user:', row);
    };
    
    const handleBlock = (row) => {
      console.log('Block user:', row);
    };
    </script>
    