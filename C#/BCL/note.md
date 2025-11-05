# Notes on Base Class Library in C# (.NET): HttpClient and Reflection

## Introduction to Base Class Library (BCL)
The Base Class Library (BCL) in .NET provides a comprehensive set of classes, interfaces, and types to support common programming tasks in C# and other .NET languages. It serves as the foundation for building applications, offering functionality for networking, file operations, data manipulation, and more. This document focuses on two key BCL components: **HttpClient** for HTTP communication and **Reflection** for runtime type inspection and manipulation.

---

## HttpClient Module
The **HttpClient** class, located in the `System.Net.Http` namespace, enables sending HTTP requests and receiving responses from resources identified by URIs. It is optimized for asynchronous operations and is commonly used for interacting with APIs and web services.

### Key Features
- Supports asynchronous HTTP requests using `async`/`await` for non-blocking operations.
- Handles various HTTP methods, including GET, POST, PUT, DELETE, and PATCH.
- Allows configuration of headers, timeouts, and base addresses.
- Designed for thread safety, enabling reuse across multiple requests.
- Supports multiple content types, such as JSON, XML, and form data.

### Key Methods and Properties
- **Methods**:
  - `GetAsync`: Sends a GET request to retrieve data.
  - `PostAsync`: Sends a POST request with content.
  - `PutAsync`: Sends a PUT request to update resources.
  - `DeleteAsync`: Sends a DELETE request to remove resources.
  - `SendAsync`: Sends a custom HTTP request with full control over the request message.
- **Properties**:
  - `BaseAddress`: Defines the base URI for all requests.
  - `DefaultRequestHeaders`: Sets headers applied to all requests.
  - `Timeout`: Specifies the maximum time to wait for a response.

### Best Practices
- Reuse a single `HttpClient` instance throughout the application to prevent socket exhaustion.
- For short-lived clients, use `using` declarations to ensure proper disposal.
- Handle HTTP errors by checking `IsSuccessStatusCode` or using `EnsureSuccessStatusCode`.
- In ASP.NET Core, use `IHttpClientFactory` for better lifecycle management and dependency injection.
- Configure timeouts to avoid indefinite waiting for responses.

---

## Reflection Module
**Reflection**, provided by the `System.Reflection` namespace, allows runtime inspection and manipulation of types, assemblies, and objects. It is useful for dynamic operations, such as inspecting metadata, invoking methods, or creating instances without compile-time knowledge.

### Key Features
- Enables inspection of type metadata, including methods, properties, fields, and constructors.
- Supports dynamic invocation of methods and access to properties or fields.
- Allows loading and exploring assemblies dynamically.
- Facilitates retrieval of custom attributes applied to types or members.
- Enables dynamic instantiation of types.

### Key Classes
- **`Type`**: Represents a type in the .NET type system.
- **`Assembly`**: Represents a loaded assembly.
- **`MethodInfo`, `PropertyInfo`, `FieldInfo`**: Represent methods, properties, and fields of a type.
- **`ConstructorInfo`**: Represents a constructor.
- **`MemberInfo`**: Base class for members like methods, properties, and fields.

### Common Operations
- Retrieve a `Type` object using `GetType` or `typeof`.
- Inspect methods, properties, or fields using `GetMethods`, `GetProperties`, or `GetFields`.
- Load assemblies dynamically with `Assembly.Load`.
- Create instances dynamically using `Activator.CreateInstance`.
- Access custom attributes with `Attribute.GetCustomAttribute`.

### Best Practices
- Use reflection sparingly due to its performance overhead.
- Cache reflection results (e.g., `Type` or `MethodInfo` objects) to avoid repeated lookups.
- Handle exceptions like `TargetInvocationException` or `MissingMethodException` gracefully.
- Avoid reflection in performance-critical code; prefer compile-time approaches when possible.
- Combine reflection with custom attributes for tasks like validation or serialization.

---

## Summary
- **HttpClient**: A versatile class for HTTP communication, optimized for asynchronous operations and reusable instances. Use `IHttpClientFactory` in ASP.NET Core for improved management.
- **Reflection**: A powerful mechanism for runtime type inspection and dynamic invocation, ideal for scenarios like plugins or serialization, but use cautiously due to performance costs.

Both components are essential to the BCL, enabling developers to create flexible, network-enabled, and dynamic applications in C#.

**References**:
- [Microsoft Docs: HttpClient](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient)
- [Microsoft Docs: Reflection](https://learn.microsoft.com/en-us/dotnet/api/system.reflection)