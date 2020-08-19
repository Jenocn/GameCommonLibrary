## GameCommonLibrary(GCL)  
针对游戏开发的C#通用方法库

### 发布页
[https://github.com/Jenocn/GameCommonLibrary/releases](https://github.com/Jenocn/GameCommonLibrary/releases)


### `GCL.Base`  
基础库  
* `ByteTool` 字节数据工具,对字节数据的处理  
* `ClassType` 类类型,类型计数,类型管理  
* `GameDate` 日期类,处理日期  
* `ParamString` 支持参数替换的字符串  
* `RandomTool` 随机数工具  
* `TypeTool` 类型工具  
* `MathTool` 数学工具
* `Utility` 其他工具

### `GCL.Net`  
网络相关方法  
依赖`GCL.Base`  
* `TCPConnect` TCP连接类,用于客户端与服务器传输数据  

### `GCL.Pattern`  
模式相关  
依赖`GCL.Base`  
* `MessageBase` 消息接口和基类定义  
* `MessageCenter` 消息中心,一个全局的消息派发器  
* `MessageDispatcher` 消息派发器类  
* `MessageListener` 消息监听器接口和基类定义  
* `SimpleNotify` 单一消息通知器
* `Singleton` 单例  

### `GCL.Serialization`  
数据序列化相关  
依赖`GCL.Base`,`GCL.Pattern`
* `CloneTool` 对象克隆工具  
* `DataObject` 数据类型对象基类定义  
* `EncryptTool` 加密工具  
* `INIReader` INI配置文件读取器  
* `INITool` INI工具,双向序列化INI配置  
* `JSONTool` JSON工具,双向序列化Json配置  
* `TableBase` 表结构基类定义  
* `TableContainer` 表容器   
* `XMLTool` XML工具,双向序列化XML配置  
