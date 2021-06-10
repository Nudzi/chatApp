class Calculator(object):
    def add(self, a, b):
        return a + b

print(Calculator.add(2, 3, 5))

class Models(object):
    def prediction(img_path):
        from keras.models import load_model
        import numpy as np # linear algebra
        from  keras.applications.vgg16 import preprocess_input
        import matplotlib.pyplot as plt
        class_list = ['bed', 'chair', 'sofa', 'swivelchair', 'table']
        model = load_model('/Users/korisnik/Desktop/furniture_images_9788.h5')
        from keras.preprocessing import image
        img_width, img_height = 224, 224

        #img_path = '/Users/korisnik/Desktop/chair.png'
        img = image.load_img(img_path, target_size=(img_height, img_width))
        img_tensor = image.img_to_array(img)
        img_tensor = np.expand_dims(img_tensor, axis=0)
        img_tensor = preprocess_input(img_tensor)

        featuremap = model.predict(img_tensor)
        index = np.argmax(featuremap)
        return (class_list[index])

print(Models.prediction('/Users/korisnik/Desktop/chair.png'))
