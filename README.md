# Grpc

### 内容
- 基于.net文档实现Grpc
- Identity和User类包，只存放proto协议，生成客户端和服务端代码：Google.Protobuf（协议）、Grpc.Tools（把协议编译成C#代码）、Grpc.Core.Api（接口）
- Identity.Api和User.Api项目引用上面对应包，继承并实现Grpc协定业务
- WebApi引用Identity和User类包，实现客户端调用

### 可补充实现内容
- 认证
- 流
