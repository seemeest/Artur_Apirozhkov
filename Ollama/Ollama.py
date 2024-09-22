
import os
import json
import asyncio
import hashlib
import logging
import aiohttp
import websockets
import requests
import base64
from pathlib import Path
from ollama import AsyncClient
from googletrans import Translator
from clarifai.client.model import Model
from clarifai.client.input import Inputs
from google.protobuf.json_format import MessageToDict

# ��������� �����������
logging.basicConfig(level=logging.INFO, format='%(asctime)s - %(levelname)s - %(message)s')

# save_path 
save_path = 'D:\Visual Studia Project\websoket_by_transformer\image'

# �������� ���������� ��� ���������� �����������, ���� ��� �� ����������
os.makedirs(save_path, exist_ok=True)

# ���������� ��� API-����
CLARIFAI_API_KEY = '9f39e35e596e4120b6eceb0fac236f9b'

# ������� ���������
message_queue = asyncio.Queue()

# ���������� ����������� � Ollama
async def process_image_messages(url, content):
    try:
        if os.path.exists(url):
            # ������������� � ������
            url_str = str(url)
            
            with open(url_str, "rb") as f:
                file_bytes = f.read()
            
            prompt = "What time of day is it?"
            inference_params = dict(temperature=0.2, max_tokens=100)

            model_prediction = Model("https://clarifai.com/openai/chat-completion/models/openai-gpt-4-vision").predict(inputs = [Inputs.get_multimodal_input(input_id="", image_bytes = file_bytes, raw_text=prompt)], inference_params=inference_params)
            print(model_prediction.outputs[0].data.text.raw)
            return model_prediction.outputs[0].data.text.raw

        else:
            logging.warning(f"���� �� ������: {url}")
            return None

    except Exception as e:
        logging.error(f"������ ��� ��������� �����������: {e}")
        return None

# ����������� �������� ����
async def download_image(url, save_path):
    try:
        hash_object = hashlib.md5(url.encode())
        unique_filename = hash_object.hexdigest() + ".jpg"
        file_path = Path(save_path) / unique_filename  # ���������� Path

        async with aiohttp.ClientSession() as session:
            async with session.get(url) as response:
                response.raise_for_status()
                with open(file_path, 'wb') as file:
                    file.write(await response.read())
        logging.info(f"����������� ������� �������: {file_path}")
        return file_path  # ���������� ������ Path
    except Exception as e:
        logging.error(f"������ ��� ���������� �����������: {e}")
        return None
    
# �������� ����
async def delete_image(filename):
    try:
        if os.path.exists(filename):
            os.remove(filename)
            logging.info(f"����������� ������� �������: {filename}")
        else:
            logging.warning(f"���� �� ������: {filename}")
    except Exception as e:
        logging.error(f"������ ��� �������� �����������: {e}")

# ������� ��� �������� ������
async def translate_text(text, dest_language='ru'):
    translator = Translator()
    translated = translator.translate(text, dest=dest_language)
    return translated.text

# �������� �������
async def process_message_queue():
    while True:
        message_id, message_text, websocket = await message_queue.get()
        try:
            logging.info(f"process_message_queue �������� ���������: {message_text}")

            stack = [message_text]
            filtered_data = []

            while stack:
                current = stack.pop()

                if isinstance(current, dict):
                    for key, value in current.items():
                        if key.lower().endswith('id'):
                            continue
                        if value is not None:
                            if key == 'Url':
                                img_path = await download_image(value, save_path)
                                if img_path is not None:
                                    processed_text = await process_image_messages(img_path, 'What is in this image?')
                                    if processed_text is not None:
                                        processed_text_translated = await translate_text(processed_text)
                                        filtered_data.append((key, processed_text_translated))
                                #await delete_image(img_path)
                            else:
                                filtered_data.append((key, value))
                                
                            if isinstance(value, (dict, list)):
                                stack.append(value)

                elif isinstance(current, list):
                    for item in current:
                        if item is not None:
                            stack.append(item)

            logging.info(f"process_message_queue ������������ ������: {filtered_data}")
            await send_response(websocket, message_id, filtered_data)

        except Exception as e:
            logging.error(f"process_message_queue ������ ��� ��������� {message_id}: {e}")
        finally:
            message_queue.task_done()

# �������� ������������� ������ ������� ����� WebSocket
async def send_response(websocket, message_id, processed_text):
    logging.info(f"send_response: {processed_text}")
    response = {'id': message_id, 'text': processed_text}
    await websocket.send(json.dumps(response))

# �������� WebSocket ������
async def handler(websocket, path):
    async for message in websocket:
        try:
            data = json.loads(message)
            logging.info(f"��������: {data}")

            message_id = data.get('id')
            message_text = data.get('text')
            if message_id and message_text:
                await message_queue.put((message_id, message_text, websocket))
            else:
                error_response = {
                    'status': 'error',
                    'message': '������������ ������ ���������� ���������',
                    'received_data': data
                }
                if websocket.open:  # ��������, ��� ���������� �������
                    await websocket.send(json.dumps(error_response))
                logging.warning(f"������������ ������ ���������� ���������: {data}")
        except json.JSONDecodeError as e:
            error_response = {
                'status': 'error',
                'message': f'������ ������������� JSON: {str(e)}'
            }
            if websocket.open:  # ��������, ��� ���������� �������
                await websocket.send(json.dumps(error_response))
            logging.error(f"������ ������������� JSON: {str(e)}")
        except Exception as e:
            error_response = {
                'status': 'error',
                'message': f'��������� ������: {str(e)}'
            }
            if websocket.open:  # ��������, ��� ���������� �������
                await websocket.send(json.dumps(error_response))
            logging.error(f"��������� ������: {str(e)}")

# ������ �������
async def start_server():
    asyncio.create_task(process_message_queue())
    
    async with websockets.serve(handler, "localhost", 8000):
        await asyncio.Future()  # �������� ���������� ������

if __name__ == "__main__":
    asyncio.run(start_server())
