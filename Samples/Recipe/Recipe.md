#AssetRails recipes
here is some patterns of AssetRails runners.   
almost all articles and tecniques are not depends on AssetRails.  
May help be with you.

##General

###merge multiple route's result
Use -e option.
Every route supports "-e --export-path" setting.
☆like below.

###load resources from specific folder
Use -s option.
Every route supports "-s --source-path" setting.
☆like below.

##in import route
###setting up image data
☆なんか例をコードで。

###import already defined settings from .meta
AssetRails can retrieve .meta files.
If you have the .meta files and the source files, AssetRails will use the information of these .meta files.

###make all resources name unique
Use -u option.





##in prefabricate route
###generate prefab object & load
☆なんか例をコードで。

###set materials to "new" prefab
☆なんか例をコードで。

###set pre-modified AnimatorController to model
☆なんか例をコードで。

###update prefab's modify
☆なんか例をコードで。

###make all resources name unique
Use -u option.




##in bundlize route

###use decompressed size of AssetBundle
Use -a option.

###generate AssetBundles for multiple platform
☆なんか例をコードで。

###generate "Not-AssetBundle" format data-bundle and make versionedList
☆なんか例をコードで。

###generate AssetBundles with dependency
☆なんか例をコードで。

###make all AssetBundle's resources name unique
Use -u option.
This will help you for making AssetBundles which is never contains same resource name for each.
If the other AssetBundles already contains same resource name, the AssetRails will stop with some kind of error and hints.

##in versioning route

###use another versionedPool for versioning
☆なんか例をコードで。

