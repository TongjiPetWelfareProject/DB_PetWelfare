<template>
    <nav class="app-topnav">
        <div class="container">
            <ul>
                <template v-if="true">
                    <li><a helf="javascript:;"><i class=" iconfont icon user"></i>Default User</a></li>
                    <li>
                        <el-popconfirm title="确认退出吗?" confirm-button-text="确认" cancel-button-text="取消">
                            <template #reference>
                                <a href="javascript:;">退出登录</a>
                            </template>
                        </el-popconfirm>
                    </li>
                </template>
                <template v-else>
                    <li><a href="javascript:;" @click="">请先登录</a></li>
                    <li><a href="javascript:;">关于我们</a></li>
                </template>
            </ul>
        </div>
    </nav>    
</template>


<style scoped lang="scss">
.app-topnav {
    background: #333;
    
    ul {
       display: flex;
       height:53px;
       justify-content: flex-end;
       align-items: center;
       
    //    li {
    //     a {

    //     }
    //    }
    }
}
</style>