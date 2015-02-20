# run routes written in json.
/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode -quit -projectPath $(pwd)\
	-executeMethod AssetRailsController.RunJson\
	-f $(pwd)/Assets/AssetRails/Samples/plan.json


# example toml.
# /Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode -quit -projectPath $(pwd)\
# 	-executeMethod AssetRailsController.RunToml\
# 	--file-path $(pwd)/Assets/AssetRails/Samples/plan.toml

