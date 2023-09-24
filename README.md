<img align="left" src="https://github.com/DevHrytsan/FlaxObjectPool/assets/55915163/3ca310e6-dbb4-4a68-a0c9-1bd1edfe3e9d" width="110px"/>
<h1>Flax Object Pool</h1>

## Description
- The recycling and reuse of objects, reducing the overhead of creating and destroying objects repeatedly.
- Easy to use
- It's also possible to use with other game engines with minor modifications.
  
## How to install
1. Open your project.
2. Navigate to Tools -> Plugins -> Clone Plugin Project.
3. Enter the following URL: https://github.com/DevHrytsan/FlaxObjectPool.git.
4. It will ask u to reload the editor. Do it.
5. Profit!
## Usage
At the beginning of your C# script, you should include the necessary namespace:
```csharp
using FlaxObjectPool;
```
To create an object pool, instantiate the BaseObjectPool class with the desired type <T>, where <T> should be a reference type (class). You can specify several parameters during initialization, including functions for creating, getting, releasing, and destroying objects, as well as default capacity, maximum size, and a flag for limiting releases. For example, I will use Actor class:
```csharp
var objectPool = new BaseObjectPool<Actor>(
    () => OnCreateFuncion(),    // Function to create objects
    obj => OnGetObject(obj),     // When getting an object
    obj => OnReleaseObject(obj), // When releasing an object
    obj => OnDestroyObject(obj)  // When destroying an object
);
```

Call the Get() method to retrieve an object from the pool. When you're done with the object, use the Release(T item) method to return it to the pool. For example:
``` csharp
Actor myActor = objectPool.Get(); //Getting from pool
objectPool.Release(myActor); //Releasing to pool
```
You can clean and dispose of objects in the pool when necessary. The Dispose() method disposes of the entire pool, while the Clean() method releases all active objects back into the pool. Use them as needed.
``` csharp
objectPool.Dispose(); // Dispose the entire pool
objectPool.Clean();  // Release all active objects back to the pool
```
You can retrieve pool-related information, including its default capacity, maximum size, the total object count in the pool, and the count of active objects, by accessing properties like DefaultCapacity, MaxSize, CountAll, and ActiveCount.
## Example 
The plugin demonstrates FlaxObjectPool usage. You'll find it in the plugin's root folder under "Content -> Demo" and open the "DemoScenePool" scene. It showcases the usage of pooling with custom projectiles. To remove the demo content, delete all the "Demo" folders in FlaxObjectPool.

https://github.com/DevHrytsan/FlaxObjectPool/assets/55915163/2d2c4d9e-47cc-48c4-94d1-ccf8ea2675bd

## Contribution
Feel free to contribute to this project and suggest some ideas for it. Don`t forget about [Flax Engine discord server](https://discord.com/invite/yFBCmY9)
## License
Under the MIT license. Feel free to use :)


