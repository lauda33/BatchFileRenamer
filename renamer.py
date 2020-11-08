# File Renamer || Python Backend

# 11.8.2020 Mehmet Tekman


import os
import json

class Renamer():


    def __init__(self):
        
        self.options = {}


    def load_options(self,json_path):

        with open(json_path) as opt:
            self.options = json.load(opt)
    

    def rename_the_path(self,path):

        # If options loaded.
        if self.options != {}:
            main_path = path
            file_names = [file_name for file_name in os.listdir(main_path)]
            file_extensions = []
            file_name_without_extensions = []
            for file_name in file_names:

                try:
                    file_ext = file_name.split(".")[1]
                    file_extensions.append(file_ext)
                    file_name_without_extensions.append(file_name.split(".")[0])

                except:
                    file_extensions.append("")
                    file_name_without_extensions.append(file_name)
            
            PATH_LEN = len(file_name_without_extensions)
            KEYWORD = self.options["keyword"]
            SEED = self.options["seed"]


            
            # Now we have main path, file names and file extensions, let's rename everything
            for  file_name,file_ext,seed in zip(file_name_without_extensions,file_extensions,range(SEED,PATH_LEN+1)):

                os.rename(main_path+file_name+"."+file_ext,main_path+str(KEYWORD)+str(seed)+"."+file_ext)




renamer = Renamer()

renamer.load_options(json_path="test.json")

renamer.rename_the_path("test\\")


# TODO - IN ORDER TO MAKE THIS SCRIPT MORE USEFUL ADD THE GUI WITH C# 

