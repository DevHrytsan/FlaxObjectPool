<img align="left" src="https://github.com/DevHrytsan/FlaxObjectPool/assets/55915163/23808ea6-9a3b-4c0b-8900-e9232af98c5a" width="110px"/>
<h1>Flax Object Pool</h1>

## Description
- The recycling and reuse of objects, reducing the overhead of creating and destroying objects repeatedly.
- Easy to use
- It's also possible to use with other game engines with minor modifications.
  
## How to install
### For Flax 1.7+ 
> [!WARNING]
> As of the current date(03.10.2023), version 1.7 corresponds to a custom build derived from both the 'master' and '1.7' branches of the Flax engine on GitHub.
> To obtain the latest version, you can either build it manually or use the Flax launcher to install the 'master' version.

1. Open your project.
2. Navigate to Tools -> Plugins -> Clone Plugin Project.
3. Enter the following URL: https://github.com/DevHrytsan/FlaxObjectPool.git.
4. It will ask you to reload the editor. Do it.
5. Profit!
### For Flax 1.6 and below
1. DON`T open your project in Editor.
2. Add FlaxObjectPool folder to the Plugin folder in your existing project
   > [!NOTE]
   > If you do not already have a Plugin folder in your project, create one.
   > Ensure that the FlaxObjectPool folder is named correctly as "FlaxObjectPool".
3. Next, add a reference from your game project to the added plugin project. Open <project_name>.flaxproj with a text editor and add a reference to the plugin project.
Like this:
``` csharp
 "References": [
      ....,
        {
            "Name": "$(ProjectPath)/Plugins/FlaxObjectPool/FlaxObjectPool.flaxproj"
        }
    ],
```
4. Open your project.
5. Enjoy!
6. 
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
You'll find it in the plugin's root folder under "Content -> Demo" and open the "DemoScenePool" scene. It showcases the usage of pooling with custom projectiles. To remove the demo content, delete all the "Demo" folders in FlaxObjectPool.

https://github.com/DevHrytsan/FlaxObjectPool/assets/55915163/dcefbc0f-9bc9-41c0-a459-404aab37291c

## Contribution
Feel free to contribute to this project and suggest some ideas for it. Don`t forget about [Flax Engine discord server](https://discord.com/invite/yFBCmY9)
## License
Under the MIT license. Feel free to use :)


