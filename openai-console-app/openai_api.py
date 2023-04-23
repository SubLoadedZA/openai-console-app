import json

def generate_text(prompt, api_key, http_client):
    headers = {
        'Content-Type': 'application/json',
        'Authorization': f'Bearer {api_key}'
    }

    data = {
        'prompt': prompt,
        'max_tokens': 50
    }

    success, response = http_client.post('https://api.openai.com/v1/engines/text-davinci-002/completions', headers=headers, data=json.dumps(data))

    if success and 'choices' in response:
        return response['choices'][0]['text']
    return f"Error generating text. Details: {response}"
