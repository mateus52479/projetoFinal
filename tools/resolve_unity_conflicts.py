import sys
from pathlib import Path

if len(sys.argv) < 2:
    print('Usage: resolve_unity_conflicts.py <path-to-unity-scene>')
    sys.exit(1)

p = Path(sys.argv[1])
if not p.exists():
    print('File not found:', p)
    sys.exit(2)

bak = p.with_suffix(p.suffix + '.bak')
if not bak.exists():
    p.replace(bak)
    bak.write_bytes(bak.read_bytes())
# Actually above replace removed file; better to copy instead

# Re-copy original to backup properly
from shutil import copy2
copy2(p, str(p) + '.bak')

lines = p.read_text(encoding='utf-8', errors='replace').splitlines()
out_lines = []
state = 'normal'
remote_buffer = []

i = 0
while i < len(lines):
    line = lines[i]
    if state == 'normal':
        if line.startswith('<<<<<<<'):
            state = 'in_head'
            i += 1
            continue
        else:
            out_lines.append(line)
            i += 1
            continue
    elif state == 'in_head':
        if line.startswith('======='):
            state = 'in_remote'
            i += 1
            continue
        else:
            i += 1
            continue
    elif state == 'in_remote':
        if line.startswith('>>>>>>>'):
            # flush remote buffer
            out_lines.extend(remote_buffer)
            remote_buffer = []
            state = 'normal'
            i += 1
            continue
        else:
            remote_buffer.append(line)
            i += 1
            continue

# Write back
p.write_text('\n'.join(out_lines) + '\n', encoding='utf-8')
print('Resolved conflicts. Lines before:', len(lines), 'after:', len(out_lines))
