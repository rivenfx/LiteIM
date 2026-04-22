# Yoyo.Pro

欢迎来到 Yoyo.Pro 仓库！🎉

Yoyo.Pro 是一个基于增强版 ASP.NET Boilerplate 的开源框架，旨在为开发者提供一个灵活且高效的解决方案，用于构建现代化的 ASP.NET 应用程序。

![Yoyo.Pro Logo](https://path-to-logo-image)

## 特性 ✨

- **灵活的主键类型**：支持 `int`、`long`、`guid` 和 `string` 类型的主键，适应各种业务场景需求。
- **无缝集成**：与现有的 ABP 基础设施无缝集成，不引入任何冲突或问题。
- **模块化设计**：模块化架构简化了开发和维护过程。
- **高性能**：优化的性能和可扩展性，适应复杂的部署场景。
- **强大的社区支持**：活跃的开发者社区，提供及时的技术支持和帮助。

## 快速开始 🚀

1. **克隆仓库**：
    ```bash
    git clone https://github.com/yoyoboot/Yoyo.Pro.git
    cd Yoyo.Pro
    ```

2. **安装依赖**：
    ```bash
    dotnet restore
    ```

3. **运行项目**：
    ```bash
    dotnet run
    ```

4. **访问应用**：
    打开浏览器并访问 [http://localhost:5000](http://localhost:5000) 以查看运行中的应用。

## 示例代码 📦

```csharp
public class MyAppService : ApplicationService
{
    private readonly IRepository<MyEntity, Guid> _myRepository;

    public MyAppService(IRepository<MyEntity, Guid> myRepository)
    {
        _myRepository = myRepository;
    }

    public async Task<MyDto> GetMyEntityAsync(Guid id)
    {
        var entity = await _myRepository.GetAsync(id);
        return ObjectMapper.Map<MyEntity, MyDto>(entity);
    }
}# YoyoBoot.Pro

yoyoboot自建的模块库